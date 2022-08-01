import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IBrand } from '../../shared/models/brand';
import { IProduct } from '../../shared/models/product';
import { ICategory } from '../../shared/models/productType';
import { ShopParams } from '../../shared/models/shopParams';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss'],
})
export class MachineComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  @ViewChild('omerlist', { static: true }) omerlist: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  shopParams: ShopParams;
  totalCount: number;
  mainCategory: string;

  constructor(
    private shopService: ShopService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.mainCategory = this.route.snapshot.url[0].path;
    this.shopParams = new ShopParams(10, this.mainCategory);
    this.getProducts();
    this.getBrands();
    this.getCategories();
  }

  generateTree(rootCategory: ICategory, rootElement: HTMLElement) {
    const processNode = (node: ICategory, element: HTMLElement, addChild = true) => {
      const div = this.renderer.createElement('div');
      const a = this.renderer.createElement('a');
      this.renderer.addClass(a, 'link-dark');
      a.innerText = node.name;

      this.renderer.listen(a, 'click', () => {
        if (addChild) {
          this.router.navigate([node.url]);
        } else {
          this.router.navigate([node.url], { relativeTo: this.route });
        }
      });

      div.appendChild(a);
      element.appendChild(div);
      if (node.childCategories?.length && addChild) {
        const ul = this.renderer.createElement('ul');
        div.appendChild(ul);
        node.childCategories.forEach((childNode) => processNode(childNode, ul, true));
      }
    };

    const root = this.renderer.createElement('ul');
    rootElement.appendChild(root);

    processNode(rootCategory, root);
  }

  onSearchProduct() {
    this.getProducts();
  }

  getProducts() {
    const findChild = (childNode: ICategory) => {
      this.shopParams.childCategoryIds.push(childNode.id);
      if (childNode.childCategories?.length) {
        childNode.childCategories.forEach((omer) => findChild(omer));
      }
    };

    this.shopService.getCategories().subscribe((response) => {
      let makine = response.find((x) => x.id == 1);
      findChild(makine);
    });

    this.shopService.getProducts(this.shopParams).subscribe(
      (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      (response) => {
        this.brands = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getCategories() {
    this.shopService.getCategories().subscribe((response) => {
      const rootElement = response.find((x) => x.url == this.mainCategory);

      // for (var i = 0; i < omer.childNodes.length; i++) {
      //   this.renderer.removeChild(this.buttonAreaElement.nativeElement, placeholders[i]);
      // }

      this.generateTree(rootElement, document.getElementById('list'));
      let omer = document.getElementById('list');
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    // this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }

  onHeaderClicked(sortText) {
    this.shopParams.sort = sortText;
    this.getProducts();
  }
}
