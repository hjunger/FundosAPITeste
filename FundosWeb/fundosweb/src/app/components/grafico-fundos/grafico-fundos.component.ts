import { Component, input } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import * as echarts from 'echarts/core';
import { BarChart } from 'echarts/charts';
import { GridComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { LineChart } from 'echarts/charts';
import { TitleComponent } from 'echarts/components';
import { CommonModule } from '@angular/common';
echarts.use([BarChart, GridComponent, CanvasRenderer, LineChart, TitleComponent]);
import { EChartsCoreOption } from 'echarts/core';
import { CotaService } from '../../services/cota.service';
import { SerieCota } from '../../domain/SerieCota';
import { SerieLinha } from '../../domain/SerieLinha';
import { toObservable } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-grafico-fundos',
  standalone: true,
  imports: [CommonModule, NgxEchartsDirective],
  templateUrl: './grafico-fundos.component.html',
  styleUrl: './grafico-fundos.component.css',
  providers: [
    provideEchartsCore({ echarts })
  ]
})
export class GraficoFundosComponent {
  signalFundoSelecionadoId = input.required<number>();
  observable$ = toObservable(this.signalFundoSelecionadoId);

  public chartOption: EChartsCoreOption = {}
  legendData: string[] = []
  legendGrid: string[] = []
  
  seriesCotas: SerieCota[] = []

  constructor(private service: CotaService)
  { }
  
  public ngOnInit(): void{
    this.observable$.subscribe((newUser) => {
      this.carregarCotas()
    });
    this.carregarCotas();
  }

  

  private carregarCotas(): void {
    this.service.getSeriesCotasUltimoAno(this.signalFundoSelecionadoId()).subscribe({
      next: (series) => {
        this.seriesCotas = series;
        this.setListas(this.seriesCotas)
      },
      error: (e) => console.error(e)
    });
  }

  private setListas(cotasCarregas: SerieCota[]): void {
    var legends = Array.from(new Set(cotasCarregas.map(c => c.fundoNome)));
    var meses: string[] = []

    if(cotasCarregas.length > 0){
      meses = cotasCarregas[0].meses
    }
    
    this.legendGrid = legends
    this.legendData = meses

    var serie: SerieLinha[] = [];
    for(var i = 0; i < cotasCarregas.length; i++){
      var item = cotasCarregas[i];
      var serieLinha: SerieLinha = {
        name: item.fundoNome,
        type: "line",
        stack: "Total",
        data: item.valores
      }

      serie.push(serieLinha)
    }

    this.carregaOptions(legends, meses, serie)
  }

  private carregaOptions(legendas: string[], gridXs: string[], serie: SerieLinha[]){
    this.chartOption = {
      title: {
        text: 'Variação das Cotas'
      },
      legend: {
        top: '5%',
        data: legendas
      },
      grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
      },
      xAxis: {
        type: 'category',
        boundaryGap: false,
        data: gridXs
      },
      yAxis: {
        type: 'value'
      },
      series: serie
    };

  }
}
