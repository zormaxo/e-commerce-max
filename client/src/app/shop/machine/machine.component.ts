import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
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
  categories: ICategory[];
  shopParams = new ShopParams(10);
  totalCount: number;

  constructor(private shopService: ShopService, private renderer: Renderer2) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  generateTree(categories: ICategory[], rootElement: HTMLElement) {
    const processNodes = (node: ICategory, element: HTMLElement, addChild = true) => {
      const div = this.renderer.createElement('div');
      const a = this.renderer.createElement('a');
      this.renderer.addClass(a, 'link-dark');
      a.innerText = node.name;

      div.appendChild(a);
      element.appendChild(div);
      if (node.childCategories?.length && addChild) {
        const ul = this.renderer.createElement('ul');
        div.appendChild(ul);
        node.childCategories.forEach((childNode) => processNodes(childNode, ul, false));
      }
    };

    const root = this.renderer.createElement('ul');
    rootElement.appendChild(root);

    const parentNodes = categories.filter((category) => category.parent == null);
    parentNodes.forEach((parentNode) => processNodes(parentNode, root));
  }

  onSearchProduct() {
    this.getProducts();
  }

  getProducts() {
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

  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.categories = response;
        this.generateTree(response, document.getElementById('list'));
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
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
