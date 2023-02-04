export class FilterAreas {
  search = true;
  isNew: boolean;
  minValue = true;
  maxValue = true;
  cityId = true;
  countyId = true;

  constructor(categoryName: string) {
    switch (categoryName) {
      case 'vehicle':
      case 'computer':
        this.isNew = true;
        this.minValue = true;
        this.maxValue = true;
        this.cityId = true;
        this.countyId = true;
        break;
      case 'cell-phone':
        this.isNew = true;
        this.minValue = true;
        this.maxValue = true;
        this.cityId = true;
        this.countyId = true;
        break;
      default:
        break;
    }
  }
}
