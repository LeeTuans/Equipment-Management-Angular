import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { routes } from './equipment-routing.module';
import { EquipmentComponent } from './equipment.component';
import { MenuBarComponent } from 'src/app/shared/components/menu-bar/menu-bar.component';
import { RouterModule } from '@angular/router';
import { ManagerComponent } from './manager/manager.component';
import { RequiredComponent } from './required/required.component';
import { InformationComponent } from './information/information.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { HeaderComponent } from 'src/app/shared/components/header/header.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DialogRequiredComponent, HomeComponent } from './home/home.component';
import { DialogComponent } from './dialog/dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { EmployeeManagerComponent } from './employee-manager/employee-manager.component';
import { DialogEmployeeComponent } from './employee-manager/dialog-employee/dialog-employee.component';
import { NgxUiLoaderModule } from 'ngx-ui-loader';

@NgModule({
  declarations: [
    EquipmentComponent,
    HomeComponent,
    ManagerComponent,
    RequiredComponent,
    InformationComponent,
    ChangePasswordComponent,
    DialogComponent,
    DialogRequiredComponent,
    EmployeeManagerComponent,
    DialogEmployeeComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    NgxUiLoaderModule,
  ],
  exports: [ManagerComponent],
})
export class EquipmentModule { }
