import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  NgxGalleryAnimation,
  NgxGalleryImage,
  NgxGalleryImageSize,
  NgxGalleryOptions,
  NgxGalleryOrder,
} from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { ICategory } from 'src/app/shared/models/category';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: Product;
  parentCategories: ICategory[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  selectedCategory: ICategory;
  currentClasses: Record<string, boolean> = {};

  constructor(public shopService: ShopService, private activatedRoute: ActivatedRoute, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.loadProduct();

    this.galleryOptions = [
      {
        width: '530px',
        height: '700px',
        imageSize: NgxGalleryImageSize.Contain,
        imageAnimation: NgxGalleryAnimation.Slide,
        imageArrowsAutoHide: false,
        imageSwipe: true,
        preview: true,
        previewCloseOnEsc: true,
        previewKeyboardNavigation: true,
        previewAnimation: false,
        thumbnailsArrowsAutoHide: true,
        thumbnailsColumns: 4,
        thumbnailsRows: 2,
        previewInfinityMove: true,
        previewBullets: true,
        thumbnailsMoveSize: 4,
        thumbnailsOrder: NgxGalleryOrder.Page,
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
        this.selectedCategory = categories.find((x: { id: number }) => x.id == this.product.categoryId);
        this.parentCategories = this.shopService.fillParentCategoryList(this.selectedCategory);
      });

      this.currentClasses = {
        'text-warning': this.product.isFavourite,
      };
    });
  }

  addLike() {
    this.shopService.AddOrRemoveFavourite(this.product.id).subscribe({
      next: () => {
        if (this.product.isFavourite) {
          this.toastr.error('Favorilerden çıkarıldı');
          this.currentClasses = {
            'text-warning': false,
          };
        } else {
          this.toastr.success('Favorilere eklendi');
          this.currentClasses = {
            'text-warning': true,
          };
        }
        this.product.isFavourite = !this.product.isFavourite;
      },
    });
  }
}
