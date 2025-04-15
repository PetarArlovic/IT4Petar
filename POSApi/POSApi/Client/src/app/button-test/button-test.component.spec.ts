import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ButtonTestComponent } from './button-test.component';

describe('ButtonTestComponent', () => {
  let component: ButtonTestComponent;
  let fixture: ComponentFixture<ButtonTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ButtonTestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ButtonTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
