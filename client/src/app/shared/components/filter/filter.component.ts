import { Component, Input, OnInit } from '@angular/core';
import { FilterAreas } from 'src/app/shop/categories/filter-areas';
import { ShopService } from 'src/app/_services/shop.service';
import { IAddress } from '../../models/address';
import { CurrencyType } from '../../models/currency';
import { ShopParams } from '../../models/shopParams';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {
  @Input() categoryName: string;
  @Input() shopParams: ShopParams;

  filterAreas: FilterAreas;
  currencyType = CurrencyType;
  cities: IAddress[];
  counties: IAddress[];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.shopService.getCities().subscribe((cities: IAddress[]) => {
      this.cities = cities;
      if (this.shopParams.cityId) {
        this.counties = this.cities.find((x) => x.id == this.shopParams.cityId)?.counties;
      }
    });

    this.filterAreas = new FilterAreas(this.categoryName);
  }

  onSearch() {
    this.shopService.searchClicked.next(this.shopParams);
  }

  onSelectChange(selectedValue: number) {
    this.counties = this.cities.find((x) => x.id == selectedValue)?.counties;
  }
}
