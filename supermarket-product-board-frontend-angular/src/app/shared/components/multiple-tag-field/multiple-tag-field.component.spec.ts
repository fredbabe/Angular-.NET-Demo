import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleTagFieldComponent } from './multiple-tag-field.component';

describe('MultipleTagFieldComponent', () => {
  let component: MultipleTagFieldComponent;
  let fixture: ComponentFixture<MultipleTagFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MultipleTagFieldComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultipleTagFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
