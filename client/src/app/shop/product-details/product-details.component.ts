import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryImageSize, NgxGalleryOptions, NgxGalleryOrder } from '@kolkov/ngx-gallery';
import { ICategory } from 'src/app/shared/models/category';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  parentCategories: ICategory[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  selectedCategory: ICategory;
  constructor(public shopService: ShopService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadProduct();

    this.galleryOptions = [
      {
        width: '530px',
        height: '700px',
        imageSize: NgxGalleryImageSize.Contain,
        imageAnimation: NgxGalleryAnimation.Slide,
        imageArrowsAutoHide: true,
        preview: true,
        previewCloseOnEsc: true,
        previewKeyboardNavigation: true,
        previewAnimation: false,
        thumbnailsArrowsAutoHide: true,
        thumbnailsColumns:4,
        thumbnailsRows:2,
        previewInfinityMove: true,
        previewBullets: true,
        thumbnailsMoveSize: 4,
        thumbnailsOrder:NgxGalleryOrder.Page,
        thumbnailSize: NgxGalleryImageSize.Contain,
      },
    ];
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.product.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url,
      });
    }
    return imageUrls;
  }

  loadProduct() {
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe((product: any) => {
      this.product = product.result;
      this.galleryImages = this.getImages();
      this.shopService.getCategories().subscribe((categories) => {
        this.selectedCategory = categories.find((x: { id: number }) => x.id == this.product.category.id);
        this.parentCategories = this.shopService.fillParentCategoryList(this.selectedCategory);
      });
    });
  }

  onFilter() {}
}
