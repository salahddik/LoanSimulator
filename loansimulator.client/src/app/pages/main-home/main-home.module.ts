import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainHomeRoutingModule } from './main-home-routing.module';
import { MainHomeComponent } from './container/main-home.component';
import { FormsModule } from '@angular/forms'; 
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../../shared/shared.module';


@NgModule({
  declarations: [MainHomeComponent,],
  imports: [
    CommonModule,
    FormsModule, SharedModule,
    MainHomeRoutingModule, HttpClientModule
  ]
})
export class MainHomeModule { }
