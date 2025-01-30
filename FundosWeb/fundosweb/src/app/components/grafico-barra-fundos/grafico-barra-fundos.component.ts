import { Component } from '@angular/core';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import * as echarts from 'echarts/core';
import { LegendComponent } from 'echarts/components';
import { BrushComponent } from 'echarts/components';
import { ToolboxComponent } from 'echarts/components';
import { TooltipComponent } from 'echarts/components';
import { BarChart } from 'echarts/charts';
import { GridComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { CommonModule } from '@angular/common';
echarts.use([LegendComponent, BrushComponent, ToolboxComponent, TooltipComponent, BarChart, GridComponent, CanvasRenderer]);
import { EChartsCoreOption } from 'echarts/core';

@Component({
  selector: 'app-grafico-barra-fundos',
  standalone: true,
  imports: [CommonModule, NgxEchartsDirective],
  templateUrl: './grafico-barra-fundos.component.html',
  styleUrl: './grafico-barra-fundos.component.css',
  providers: [
    provideEchartsCore({ echarts }),
  ]
})

export class GraficoBarraFundosComponent {
  xAxisData: string[] = [];
  data1: number[] = [];
  data2: number[] = [];
  data3: number[] = [];
  data4: number[] = [];
  

  emphasisStyle = {
    itemStyle: {
      shadowBlur: 10,
      shadowColor: 'rgba(0,0,0,0.3)'
    }
  };

  chartOption: EChartsCoreOption = {
    legend: {
      data: ['bar', 'bar2', 'bar3', 'bar4'],
      left: '10%',
      top: '5%'
    },
    tooltip: {},
    xAxis: {
      data: this.xAxisData,
      name: 'X Axis',
      axisLine: { onZero: true },
      splitLine: { show: false },
      splitArea: { show: false }
    },
    yAxis: {},
    grid: {
      bottom: 100
    },
    series: [
      {
        name: 'bar',
        type: 'bar',
        stack: 'one',
        emphasis: this.emphasisStyle,
        data: this.data1
      },
      {
        name: 'bar2',
        type: 'bar',
        stack: 'one',
        emphasis: this.emphasisStyle,
        data: this.data2
      },
      {
        name: 'bar3',
        type: 'bar',
        stack: 'two',
        emphasis: this.emphasisStyle,
        data: this.data3
      },
      {
        name: 'bar4',
        type: 'bar',
        stack: 'two',
        emphasis: this.emphasisStyle,
        data: this.data4
      }
    ]
  }  

  public ngOnInit(): void{    
    for (let i = 0; i < 10; i++) {
      this.xAxisData.push('Class' + i);
      this.data1.push(+(Math.random() * 2).toFixed(2));
      this.data2.push(+(Math.random() * 5).toFixed(2));
      this.data3.push(+(Math.random() + 0.3).toFixed(2));
      this.data4.push(+Math.random().toFixed(2));
    }
  }  
}
