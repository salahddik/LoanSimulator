import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainNotFound404RoutingModule } from './main-not-found404-routing.module';
import { NotFound404Component } from './container/not-found404.component';


@NgModule({
  declarations: [NotFound404Component],
  imports: [
    CommonModule,
    MainNotFound404RoutingModule
  ]
})
export class MainNotFound404Module { }
