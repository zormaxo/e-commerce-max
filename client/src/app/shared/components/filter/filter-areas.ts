export class FilterAreas {
  search = true;
  isNew: boolean;
  minValue: boolean;
  maxValue: boolean;
  cityId: boolean;
  countyId: boolean;

  constructor(categoryName: string) {
    switch (categoryName) {
      case 'malzeme':
      case 'makine':
        this.isNew = true;
        this.minValue = true;
        this.maxValue = true;
        this.cityId = true;
        this.countyId = true;
        break;
      case 'yari-mamul':
        this.minValue = true;
        this.maxValue = true;
        this.cityId = true;
        this.countyId = true;
      default:
        break;
    }
  }
}
