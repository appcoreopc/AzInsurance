import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HouseInsuranceComponent } from './house-insurance.component';

describe('HouseInsuranceComponent', () => {
  let component: HouseInsuranceComponent;
  let fixture: ComponentFixture<HouseInsuranceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HouseInsuranceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HouseInsuranceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
