import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RushingTableComponent } from './rushing-table.component';

describe('RushingTableComponent', () => {
  let component: RushingTableComponent;
  let fixture: ComponentFixture<RushingTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RushingTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RushingTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
