import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainAboutRoutingModule } from './main-about-routing.module';
import { MainAboutComponent } from './container/main-about.component';


@NgModule({
  declarations: [MainAboutComponent
],
  imports: [
    CommonModule,
    MainAboutRoutingModule
  ]
})
export class MainAboutModule { }
