import {
  Component,
  OnInit,
  signal,
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

import {
  DisplayGrid,
  GridsterComponent,
  GridsterConfig,
  GridsterItemComponent,
  GridType
} from 'angular-gridster2';

import { ListaFundosComponent } from "../lista-fundos/lista-fundos.component";
import { GraficoFundosComponent } from "../grafico-fundos/grafico-fundos.component";
import { GraficoBarraFundosComponent } from "../grafico-barra-fundos/grafico-barra-fundos.component";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [GridsterComponent, GridsterItemComponent, MatIconModule, ListaFundosComponent, GraficoFundosComponent, GraficoBarraFundosComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})

export class DashboardComponent implements OnInit {

  options!: GridsterConfig;
  signalFundoSelecionadoId = signal<number>(0);

  ngOnInit(){
    this.signalFundoSelecionadoId.set(0)
    this.options = {
      gridType: GridType.Fit,
      displayGrid: DisplayGrid.Always,
      // fixedColWidth: 105,
      // fixedRowHeight: 105,
      keepFixedHeightInMobile: false,
      keepFixedWidthInMobile: false,
      mobileBreakpoint: 640,
      useBodyForBreakpoint: false,
      pushItems: true,
      rowHeightRatio: 1,
      draggable: {
        enabled: true
      },
      resizable: {
        enabled: true
      }
    };
  }

  changedOptions(): void {
    if (this.options.api && this.options.api.optionsChanged) {
      this.options.api.optionsChanged();
    }
  }

  changeFundoId(fundoId: number) {
    this.signalFundoSelecionadoId.set(fundoId)
  }
}
