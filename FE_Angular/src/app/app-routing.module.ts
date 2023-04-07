import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./module/layout/layout.module').then(m => m.LayoutModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'login',
    loadChildren: () =>
      import('./module/login/login.module').then((m) => m.LoginModule),
    canActivate: [AuthGuard],
    data: {
      page: 'login',
    },
  },
  {
    path: '**',
    redirectTo: '/required',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
