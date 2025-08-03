import {  provideHttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MainFooterComponent} from './components/main-footer/main-footer.component';
import {MainNavbarComponent} from './components/main-navbar/main-navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    MainFooterComponent,
    MainNavbarComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
  ],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }
