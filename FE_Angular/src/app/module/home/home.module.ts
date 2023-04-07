import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { DialogRequiredComponent, HomeComponent } from './home.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    HomeComponent, DialogRequiredComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MatDialogModule,
    SharedModule
  ]
})
export class HomeModule { }
