import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogRequiredComponent } from './dialog-required.component';

describe('DialogRequiredComponent', () => {
  let component: DialogRequiredComponent;
  let fixture: ComponentFixture<DialogRequiredComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogRequiredComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DialogRequiredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
