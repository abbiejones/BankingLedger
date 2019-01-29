import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserMenuComponent } from './usermenu/usermenu.component';
import { AuthGuard }                          from './auth/auth.guard';
//import { SelectivePreloadingStrategyService } from './selective-preloading-strategy.service';

export const appRoutes: Routes = [
  {
      path: 'login',
      component: LoginComponent
  },
  {
      path: 'register',
      component: RegisterComponent
  },
  {
    path: 'usermenu',
    component: UserMenuComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'logout',
    component: LoginComponent
  },
  {
      path: '',
      redirectTo: '/login',
      pathMatch: 'full'
  },
  {
      path: '**',
      redirectTo: '/login',
      pathMatch: 'full'
  }
];


@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      {
        enableTracing: false, // <-- debugging purposes only
        //preloadingStrategy: SelectivePreloadingStrategyService,
      }
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }

