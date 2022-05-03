import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  users: unknown;
  products: IProduct[];

  constructor(private http: HttpClient, private accountService: AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe({
      next(response: IPagination) {
        this.products = response.data;
      },
      error(error) {
        console.log(error);
      },
    });
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }
}
