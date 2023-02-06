import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../shared/models/api-response/api-response';
import { Order } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getOrdersForUser() {
    return this.http.get<ApiResponse<Order[]>>(this.baseUrl + 'orders');
  }
  getOrderDetailed(id: number) {
    return this.http.get<ApiResponse<Order>>(this.baseUrl + 'orders/' + id);
  }
}
