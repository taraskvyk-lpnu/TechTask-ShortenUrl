import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUrlFormComponent } from './add-url-form.component';

describe('AddUrlFormComponent', () => {
  let component: AddUrlFormComponent;
  let fixture: ComponentFixture<AddUrlFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddUrlFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUrlFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
