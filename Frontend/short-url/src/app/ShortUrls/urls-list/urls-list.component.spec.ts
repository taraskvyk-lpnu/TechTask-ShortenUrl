import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UrlsListComponent } from './urls-list.component';

describe('UrlsListComponent', () => {
  let component: UrlsListComponent;
  let fixture: ComponentFixture<UrlsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UrlsListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UrlsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
