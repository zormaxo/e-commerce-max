import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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

  constructor(private shopService: ShopService) {}

  omercat = [
    { id: 1, categoryName: 'Root', parentId: 0 },
    { id: 2, categoryName: 'Cat1', parentId: 1 },
    { id: 3, categoryName: 'Cat2', parentId: 2 },
    { id: 4, categoryName: 'Cat3', parentId: 5 },
    { id: 5, categoryName: 'Cat4', parentId: 1 },
    { id: 6, categoryName: 'Cat5', parentId: 5 },
    { id: 7, categoryName: 'Cat6', parentId: 5 },
    { id: 8, categoryName: 'Cat7', parentId: 1 },
    { id: 9, categoryName: 'Cat8', parentId: 2 },
    { id: 10, categoryName: 'Cat9', parentId: 1 },
    { id: 11, categoryName: 'Cat10', parentId: 10 },
    { id: 12, categoryName: 'Cat11', parentId: 1 },
    { id: 13, categoryName: 'Cat12', parentId: 8 },
    { id: 6, categoryName: 'qwer', parentId: 5 },
  ];

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  generateTree(categories: ICategory[], rootElement: HTMLElement) {
    const parentNodes = categories.filter((category) => category.parent == null);
    parentNodes.forEach((parentNode) => processNodes(parentNode, rootElement));

    function processNodes(node: ICategory, element: HTMLElement) {
      const li = document.createElement('li');
      li.innerText = node.name;
      element.appendChild(li);
      if (node.childCategories?.length) {
        const ul = document.createElement('ul');
        li.appendChild(ul);
        node.childCategories.forEach((childNode) => processNodes(childNode, ul));
      }
    }
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
