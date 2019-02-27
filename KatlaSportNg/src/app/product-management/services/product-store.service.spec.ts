import { TestBed, inject } from '@angular/core/testing';

import { ProductStoreService } from './product-store.service';

describe('StoreProductService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProductStoreService]
    });
  });

  it('should be created', inject([ProductStoreService], (service: ProductStoreService) => {
    expect(service).toBeTruthy();
  }));
});
