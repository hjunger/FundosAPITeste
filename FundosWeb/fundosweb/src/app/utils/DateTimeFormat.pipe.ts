import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from './constants';

@Pipe({
  name: 'DateFormatPipe',
  standalone: true
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {
  override transform(value: any, args?: any): any {
    return super.transform(value, Constants.DATE_FMT);
  }
}