import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../services/product.service';
import { HiveSectionService } from 'app/hive-management/services/hive-section.service';
import { ProductRequestService } from '../services/product-request.service';
import { HiveSectionListItem } from 'app/hive-management/models/hive-section-list-item';
import { ProductListItem } from '../models/product-list-item';
import { StoreItemRequest } from '../models/store-item-request';

@Component({
  selector: 'app-product-request-form',
  templateUrl: './product-request-form.component.html',
  styleUrls: ['./product-request-form.component.css']
})
export class ProductRequestFormComponent implements OnInit {

  hiveSections: HiveSectionListItem[];
  products: ProductListItem[];
  request = new StoreItemRequest(0, 0, 0, 0, false);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private hiveSectionService: HiveSectionService,
    private productService: ProductService,
    private productRequestService: ProductRequestService
  ) { }

  ngOnInit() {
    this.hiveSectionService.getHiveSections().subscribe(
      hs => this.hiveSections = hs
    );

    this.productService.getProducts().subscribe(
      p => this.products = p
    );

    this.route.params.subscribe(p => {
      if (p['id'] == undefined) {
        if (p['hiveSectionId'] != undefined) {
          this.request.hiveSectionId = p['hiveSectionId'];
          this.request.productId = this.products[0].id;
          this.request.quantity = 1;
        }
        return;
      }
    });
  }

  onSubmit() {
    this.productRequestService.addRequest(this.request).subscribe(
      s => {
        this.router.navigate([`/store/${s.hiveSectionId}`]);
      });
  }

  navigateToProducts() {
    this.router.navigate([`store/${this.request.hiveSectionId}`]);
  }

  onCancel() {
    this.navigateToProducts();
  }
}
