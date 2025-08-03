import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainAlldataComponent } from './container/main-alldata.component';

const routes: Routes = [
  {
    path: '',
    component: MainAlldataComponent
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainalldataRoutingModule { }
