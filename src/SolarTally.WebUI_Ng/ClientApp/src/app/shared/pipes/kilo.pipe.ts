import { Pipe, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Pipe({
  name: 'kilo'
})
export class KiloPipe implements PipeTransform {

  constructor(private decimalPipe: DecimalPipe) {

  }

  transform(value: any, digits?: any): any {
    return this.decimalPipe.transform(value/1000, digits)
  }

}
