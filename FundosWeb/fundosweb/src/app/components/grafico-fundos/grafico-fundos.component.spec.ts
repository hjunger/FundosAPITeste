import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraficoFundosComponent } from './grafico-fundos.component';

describe('GraficoFundosComponent', () => {
  let component: GraficoFundosComponent;
  let fixture: ComponentFixture<GraficoFundosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GraficoFundosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GraficoFundosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
