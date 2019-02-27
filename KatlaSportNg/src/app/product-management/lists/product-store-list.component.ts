import { Component, OnInit } from '@angular/core';
import { StoreItem } from '../models/store-item';
import { ProductStoreService } from '../services/product-store.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-store-list',
  templateUrl: './product-store-list.component.html',
  styleUrls: ['./product-store-list.component.css']
})
export class ProductStoreListComponent implements OnInit {

  storeItems: StoreItem[];
  hiveSectionId: number;

  constructor(
    private route: ActivatedRoute,
    private storeService: ProductStoreService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.hiveSectionId = p['hiveSectionId'];
      this.storeService.getProducts(this.hiveSectionId).subscribe(s => this.storeItems = s);
    });
  }

}
