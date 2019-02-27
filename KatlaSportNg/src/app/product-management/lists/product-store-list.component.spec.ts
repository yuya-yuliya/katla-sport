import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductStoreListComponent } from './product-store-list.component';

describe('ProductStoreListComponent', () => {
  let component: ProductStoreListComponent;
  let fixture: ComponentFixture<ProductStoreListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductStoreListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductStoreListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
