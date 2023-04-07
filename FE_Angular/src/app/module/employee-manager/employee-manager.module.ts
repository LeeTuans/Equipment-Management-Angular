import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeManagerRoutingModule } from './employee-manager-routing.module';
import { EmployeeManagerComponent } from './employee-manager.component';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogEmployeeComponent } from './dialog-employee/dialog-employee.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    EmployeeManagerComponent, DialogEmployeeComponent
  ],
  imports: [
    CommonModule,
    EmployeeManagerRoutingModule,
    MatDialogModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class EmployeeManagerModule { }
