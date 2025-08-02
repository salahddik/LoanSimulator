import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainHomeComponent} from './pages/main-home/main-home.component';
import { NotFound404Component } from './pages/not-found404/not-found404.component';
import { MainAboutComponent } from './pages/main-about/main-about.component';

const routes: Routes = [
  {
    path: '',
    component: MainHomeComponent
  },
  {
    path: 'about',
    component: MainAboutComponent
  },
  {
    path: '**',
    component: NotFound404Component
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
