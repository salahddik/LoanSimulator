import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'madCurrency',
  standalone: false
})
export class MadCurrencyPipe implements PipeTransform {

  transform(value: unknown): string {
    if (typeof value !== 'number') {
      return '';
    }
    return value.toLocaleString('fr-MA', {
      style: 'currency',
      currency: 'MAD'
    });
  }

}
