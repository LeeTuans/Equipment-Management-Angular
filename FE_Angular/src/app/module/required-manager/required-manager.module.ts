import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RequiredManagerRoutingModule } from './required-manager-routing.module';
import { RequiredManagerComponent } from './required-manager.component';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { SharedModule } from 'src/app/shared/shared.module';
import { DialogRequiredComponent } from './dialog-required/dialog-required.component';


@NgModule({
  declarations: [
    RequiredManagerComponent,
    DialogRequiredComponent
  ],
  imports: [
    CommonModule,
    RequiredManagerRoutingModule,
    NgxUiLoaderModule,
    SharedModule
  ]
})
export class RequiredManagerModule { }
