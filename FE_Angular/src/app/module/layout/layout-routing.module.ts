import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleGuard } from 'src/app/guards/role.guard';
import { LayoutComponent } from './layout.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [RoleGuard],
    children: [
      {
        path: '',
        loadChildren: () => import('../home/home.module').then(m => m.HomeModule),
      },
      {
        path: 'home',
        loadChildren: () => import('../home/home.module').then(m => m.HomeModule),
      },
      {
        path: 'equipment',
        loadChildren: () => import('../equipment-manager/equipment-manager.module').then(m => m.EquipmentManagerModule),
      },
      {
        path: 'required',
        loadChildren: () => import('../required-manager/required-manager.module').then(m => m.RequiredManagerModule),
      },
      {
        path: 'employee',
        loadChildren: () => import('../employee-manager/employee-manager.module').then(m => m.EmployeeManagerModule),
      },
    ],
  },
  {
    path: 'account',
    component: LayoutComponent,
    canActivate: [RoleGuard],
    children: [
      {
        path: 'profile',
        loadChildren: () => import('../profile/profile.module').then(m => m.ProfileModule),
      },
      {
        path: 'change-password',
        loadChildren: () => import('../change-password/change-password.module').then(m => m.ChangePasswordModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
