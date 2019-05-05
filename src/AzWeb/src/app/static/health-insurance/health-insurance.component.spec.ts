import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthInsuranceComponent } from './health-insurance.component';

describe('HealthInsuranceComponent', () => {
  let component: HealthInsuranceComponent;
  let fixture: ComponentFixture<HealthInsuranceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthInsuranceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthInsuranceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
