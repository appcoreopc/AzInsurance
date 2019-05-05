import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarInsuranceComponent } from './car-insurance.component';

describe('CarInsuranceComponent', () => {
  let component: CarInsuranceComponent;
  let fixture: ComponentFixture<CarInsuranceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarInsuranceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarInsuranceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
