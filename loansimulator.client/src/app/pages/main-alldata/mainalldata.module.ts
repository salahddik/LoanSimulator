import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainalldataRoutingModule } from './mainalldata-routing.module';
import { MainAlldataComponent } from './container/main-alldata.component';
import { SharedModule } from '../../shared/shared.module';
import { provideHttpClient } from '@angular/common/http';


@NgModule({
  declarations: [MainAlldataComponent,],
  imports: [
    CommonModule, SharedModule,
    MainalldataRoutingModule
  ]
  , providers: [provideHttpClient()],

})
export class MainalldataModule { }
