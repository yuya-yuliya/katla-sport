import { TestBed, inject } from '@angular/core/testing';

import { ProductRequestService } from './product-request.service';

describe('ProductRequestService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProductRequestService]
    });
  });

  it('should be created', inject([ProductRequestService], (service: ProductRequestService) => {
    expect(service).toBeTruthy();
  }));
});
