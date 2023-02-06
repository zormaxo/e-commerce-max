import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../shared/models/api-response/api-response';
import { DeliveryMethod } from '../shared/models/deliveryMethod';
import { OrderToCreate, Order } from '../shared/models/order';


@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createOrder(order: OrderToCreate) {
    return this.http.post<Order>(this.baseUrl + 'orders', order);
  }

  getDeliveryMethods() {
    return this.http.get<ApiResponse<DeliveryMethod[]>>(this.baseUrl + 'orders/deliveryMethods').pipe(
      map((dm) => {
        return dm.result.sort((a, b) => b.price - a.price);
      })
    );
  }
}
