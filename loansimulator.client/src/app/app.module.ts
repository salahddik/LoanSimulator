import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MainFooterComponent} from './components/main-footer/main-footer.component';
import {MainNavbarComponent} from './components/main-navbar/main-navbar.component';
import {MainHomeComponent} from './pages/main-home/main-home.component';
import {FormsModule} from '@angular/forms';
import { MadCurrencyPipe } from './shared/pipes/mad-currency.pipe';
import { NotFound404Component } from './pages/not-found404/not-found404.component';
import { MainAboutComponent } from './pages/main-about/main-about.component';

@NgModule({
  declarations: [
    AppComponent,
    MainHomeComponent,
    MainFooterComponent,
    MainNavbarComponent,
    MadCurrencyPipe,
    NotFound404Component,
    MainAboutComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
