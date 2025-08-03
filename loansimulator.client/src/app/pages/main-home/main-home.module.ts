import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainHomeRoutingModule } from './main-home-routing.module';
import { MainHomeComponent } from './container/main-home.component';
import { FormsModule } from '@angular/forms'; 
import { SharedModule } from '../../shared/shared.module';
import { provideHttpClient } from '@angular/common/http';


@NgModule({
  declarations: [MainHomeComponent,],
  imports: [
    CommonModule,
    FormsModule, SharedModule,
    MainHomeRoutingModule,
  ]
  , providers: [provideHttpClient()],

})
export class MainHomeModule { }
