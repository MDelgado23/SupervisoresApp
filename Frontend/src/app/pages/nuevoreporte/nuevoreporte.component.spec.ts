import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NuevoreporteComponent } from './nuevoreporte.component';

describe('NuevoreporteComponent', () => {
  let component: NuevoreporteComponent;
  let fixture: ComponentFixture<NuevoreporteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NuevoreporteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NuevoreporteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
