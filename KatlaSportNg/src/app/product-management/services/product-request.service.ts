import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StoreItemRequest } from '../models/store-item-request';

@Injectable({
  providedIn: 'root'
})
export class ProductRequestService {
  private url = environment.apiUrl + 'api/requests/';

  constructor(
    private http: HttpClient
  ) { }

  addRequest(request: StoreItemRequest): Observable<StoreItemRequest> {
    return this.http.post<StoreItemRequest>(`${this.url}`, request);
  }
}
