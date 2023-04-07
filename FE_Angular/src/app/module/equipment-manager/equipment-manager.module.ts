import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EquipmentManagerRoutingModule } from './equipment-manager-routing.module';
import { EquipmentManagerComponent } from './equipment-manager.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DialogEquipmentComponent } from './dialog-equipment/dialog-equipment.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FilterPipe } from './pipe/filter.pipe';
import { SortDirective } from './directive/sort.directive';


@NgModule({
  declarations: [
    EquipmentManagerComponent,
    DialogEquipmentComponent,
    FilterPipe,
    SortDirective
  ],
  imports: [
    CommonModule,
    EquipmentManagerRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class EquipmentManagerModule { }
