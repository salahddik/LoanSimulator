import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainAlldataComponent } from './main-alldata.component';

describe('MainAlldataComponent', () => {
  let component: MainAlldataComponent;
  let fixture: ComponentFixture<MainAlldataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MainAlldataComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainAlldataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
