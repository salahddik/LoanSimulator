import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/main-home/main-home.module').then(m => m.MainHomeModule)
  },
  {
    path: 'about',
    loadChildren: () => import('./pages/main-about/main-about.module').then(m => m.MainAboutModule)
  }
  ,
    {
      path: 'alldata',
      loadChildren: () => import('./pages/main-alldata/mainalldata.module').then(m => m.MainalldataModule)
  }
 ,
  {
    path: '**',
    loadChildren: () => import('./pages/not-found404/main-not-found404.module').then(m => m.MainNotFound404Module)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
