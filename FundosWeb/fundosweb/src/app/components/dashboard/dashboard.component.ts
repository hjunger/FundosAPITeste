import { Component } from '@angular/core';
import {GridsterComponent, GridsterItemComponent} from 'angular-gridster2';
import { GridsterConfig, GridsterItem }  from 'angular-gridster2';
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [GridsterComponent, GridsterItemComponent, MatIconModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})

export class DashboardComponent {
  options!: GridsterConfig;
  dashboard!: Array<GridsterItem>;

  static itemChange(item: any, itemComponent: any) {
    console.info('itemChanged', item, itemComponent);
  }

  static itemResize(item: any, itemComponent: any) {
    console.info('itemResized', item, itemComponent);
  }

  ngOnInit(){
    this.options = {
      itemChangeCallback: DashboardComponent.itemChange,
      itemResizeCallback: DashboardComponent.itemResize,
    };

    this.dashboard = [
      {cols: 2, rows: 1, y: 0, x: 0},
      {cols: 2, rows: 2, y: 0, x: 2}
    ];
  }

  removeItem($event: MouseEvent | TouchEvent, item: GridsterItem): void {
    $event.preventDefault();
    $event.stopPropagation();
    this.dashboard.splice(this.dashboard.indexOf(item), 1);
  }
}
