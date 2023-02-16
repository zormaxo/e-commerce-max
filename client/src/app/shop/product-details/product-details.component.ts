import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {
  NgxGalleryAnimation,
  NgxGalleryImage,
  NgxGalleryImageSize,
  NgxGalleryOptions,
  NgxGalleryOrder,
} from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, mergeMap, Subscription, take, tap, throttleTime } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { ICategory } from 'src/app/shared/models/category';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild('favorite') favorite: ElementRef;
  @ViewChildren('favorite') domReference: QueryList<NgModel>;

  product?: Product;
  parentCategories: ICategory[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  selectedCategory: ICategory;
  currentClasses: Record<string, boolean> = {};

  quantity = 1;
  quantityInBasket = 0;
  domSubscription: Subscription;
  eventSubscription: Subscription;

  constructor(
    public shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private basketService: BasketService,
    private toastr: ToastrService
  ) {}

  ngAfterViewInit(): void {
    let favorite: HTMLElement;

    const subscribeToEvent = () => {
      this.eventSubscription = fromEvent(favorite, 'click')
        .pipe(
          throttleTime(1000),
          tap(() => {
            this.addLike();
          })
        )
        .subscribe();
    };

    if (this.favorite) {
      favorite = this.favorite.nativeElement;
      subscribeToEvent();
    } else {
      this.domSubscription = this.domReference.changes.subscribe((comps) => {
        favorite = comps.first.nativeElement;
        subscribeToEvent();
      });
    }
  }

  ngOnInit(): void {
    this.loadProduct();

    this.galleryOptions = [
      {
        width: '502px',
        height: '550px',
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
        imagePercent: 70,
        thumbnailsPercent: 30,
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
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id)
      this.shopService
        .getProduct(+id)
        .pipe(
          mergeMap((product) => {
            this.product = product;
            this.galleryImages = this.getImages();
            this.currentClasses = {
              'text-warning': this.product.isFavourite,
            };
            return this.basketService.basketSource$.pipe(take(1));
          })
        )
        .subscribe({
          next: (basket) => {
            const item = basket?.items.find((x) => x.id === +id);
            if (item) {
              this.quantity = item.quantity;
              this.quantityInBasket = item.quantity;
            }
            this.shopService.getCategories().subscribe((categories) => {
              this.selectedCategory = categories.find((x: { id: number }) => x.id == this.product.categoryId);
              this.parentCategories = this.shopService.fillParentCategoryList(this.selectedCategory);
            });
          },
          error: (error) => console.log(error),
        });
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    this.quantity--;
  }

  updateBasket() {
    if (this.product) {
      if (this.quantity > this.quantityInBasket) {
        const itemsToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketService.addItemToBasket(this.product, itemsToAdd);
      } else {
        const itemsToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= itemsToRemove;
        this.basketService.removeItemFromBasket(this.product.id, itemsToRemove);
      }
    }
  }

  get buttonText() {
    return this.quantityInBasket === 0 ? 'Add to basket' : 'Update basket';
  }

  addLike() {
    this.shopService.addOrRemoveFavourite(this.product.id).subscribe({
      next: () => {
        if (this.product.isFavourite) {
          this.toastr.error('Removed from favorites');
          this.currentClasses = {
            'text-warning': false,
          };
        } else {
          this.toastr.success('Added to favorites');
          this.currentClasses = {
            'text-warning': true,
          };
        }
        this.product.isFavourite = !this.product.isFavourite;
      },
    });
  }

  ngOnDestroy(): void {
    if (this.domSubscription) {
      this.domSubscription.unsubscribe();
    }
    if (this.eventSubscription) {
      this.eventSubscription.unsubscribe();
    }
  }
}
