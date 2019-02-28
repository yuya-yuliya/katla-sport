import { Component, OnInit } from '@angular/core';
import { StoreItem } from '../models/store-item';
import { ProductStoreService } from '../services/product-store.service';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../services/product.service';
import { ProductListItem } from '../models/product-list-item';

@Component({
  selector: 'app-product-store-list',
  templateUrl: './product-store-list.component.html',
  styleUrls: ['./product-store-list.component.css']
})
export class ProductStoreListComponent implements OnInit {

  storeItems: StoreItem[];
  products: ProductListItem[];
  hiveSectionId: number;

  constructor(
    private route: ActivatedRoute,
    private storeService: ProductStoreService,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.productService.getProducts().subscribe(p => this.products = p);

    this.route.params.subscribe(p => {
      this.hiveSectionId = p['hiveSectionId'];
      this.storeService.getProducts(this.hiveSectionId).subscribe(s => this.storeItems = s);
    });
  }

  getProductName(productId): string {
    return this.products.find(p => p.id == productId).name;
  }
}
