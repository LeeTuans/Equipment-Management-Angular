import { Routes } from '@angular/router';
import { RoleGuard } from 'src/app/guards/role.guard';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { EmployeeManagerComponent } from './employee-manager/employee-manager.component';
import { EquipmentComponent } from './equipment.component';
import { HomeComponent } from './home/home.component';
import { InformationComponent } from './information/information.component';
import { ManagerComponent } from './manager/manager.component';
import { RequiredComponent } from './required/required.component';

export const routes: Routes = [
  {
    path: '',
    component: EquipmentComponent,
    canActivate: [RoleGuard],
    children: [
      {
        path: '',
        component: HomeComponent,
      },
      {
        path: 'home',
        loadChildren: () => import('../home/home.module').then(m => m.HomeModule),
      },
      {
        path: 'management',
        component: ManagerComponent,
      },
      {
        path: 'required',
        component: RequiredComponent,
      },
      {
        path: 'employee',
        component: EmployeeManagerComponent,
      },
    ],
  },
  {
    path: 'account/profile',
    component: EquipmentComponent,
    children: [
      {
        path: '',
        outlet: 'profile',
        component: InformationComponent,
        pathMatch: 'full',
      },
    ],
  },
  {
    path: 'account/change-password',
    component: EquipmentComponent,
    children: [
      {
        path: '',
        outlet: 'change-password',
        component: ChangePasswordComponent,
        pathMatch: 'full',
      },
    ],
  },
];
