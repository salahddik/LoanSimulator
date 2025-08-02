import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainHomeRoutingModule } from './main-home-routing.module';
import { MainHomeComponent } from './container/main-home.component';
import { MadCurrencyPipe } from '../../shared/pipes/mad-currency.pipe';
import { FormsModule } from '@angular/forms'; 
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [MainHomeComponent, MadCurrencyPipe],
  imports: [
    CommonModule,
    FormsModule, 
    MainHomeRoutingModule, HttpClientModule
  ]
})
export class MainHomeModule { }
