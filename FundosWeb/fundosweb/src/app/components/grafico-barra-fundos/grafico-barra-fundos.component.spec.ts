import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraficoBarraFundosComponent } from './grafico-barra-fundos.component';

describe('GraficoBarraFundosComponent', () => {
  let component: GraficoBarraFundosComponent;
  let fixture: ComponentFixture<GraficoBarraFundosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GraficoBarraFundosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GraficoBarraFundosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
