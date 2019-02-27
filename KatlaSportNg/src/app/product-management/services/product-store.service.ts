import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StoreItem } from '../models/store-item';

@Injectable({
  providedIn: 'root'
})
export class ProductStoreService {
  private url = environment.apiUrl + 'api/store/';

  constructor(
    private http: HttpClient
  ) { }

  getProducts(hiveSectionId: number): Observable<Array<StoreItem>> {
    return this.http.get<Array<StoreItem>>(`${this.url}${hiveSectionId}`);
  }
}
