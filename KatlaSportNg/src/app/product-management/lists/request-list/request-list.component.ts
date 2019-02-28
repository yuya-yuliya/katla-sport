import { Component, OnInit } from '@angular/core';
import { StoreItemRequest } from 'app/product-management/models/store-item-request';
import { ProductRequestService } from 'app/product-management/services/product-request.service';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css']
})
export class RequestListComponent implements OnInit {

  requests: StoreItemRequest[];

  constructor(
    private productRequestService: ProductRequestService
  ) { }

  ngOnInit() {
    this.productRequestService.getRequests().subscribe(r => this.requests = r);
  }

  onComplete(requestId: number) {
    var request = this.requests.find(r => r.id == requestId);
    this.productRequestService.setCompletedStatus(requestId).subscribe(r => request.completed = true);
  }
}
