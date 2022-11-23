export class FilterAreas {
  search = true;
  isNew = true;
  minValue = true;
  maxValue = true;
  cityId = true;
  countyId = true;

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
        break;
      default:
        break;
    }
  }
}
