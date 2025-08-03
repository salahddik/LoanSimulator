import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MadCurrencyPipe } from './pipes/mad-currency.pipe';



@NgModule({
  declarations: [MadCurrencyPipe],
  imports: [
    CommonModule
  ],
  exports: [MadCurrencyPipe]
})
export class SharedModule { }
