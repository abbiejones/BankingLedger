import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserMenuComponent } from './usermenu/usermenu.component';
import { FormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {MatDialogModule, MatNativeDateModule, MatFormFieldModule} from '@angular/material';
//import { UserDialogsComponent } from './usermenu/userdialogs/userdialogs.component';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { BalanceDialog, TransactionDialog, WithdrawDialog, DepositDialog} from '../app/usermenu/userdialogs/userdialogs.component'
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    UserMenuComponent,
    BalanceDialog,
    DepositDialog,
    WithdrawDialog,
    TransactionDialog
  ],
  entryComponents: [
    BalanceDialog,   
    DepositDialog,
    WithdrawDialog,
    TransactionDialog],

  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatNativeDateModule,
    MatDialogModule,
    MatFormFieldModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }


platformBrowserDynamic().bootstrapModule(AppModule);
