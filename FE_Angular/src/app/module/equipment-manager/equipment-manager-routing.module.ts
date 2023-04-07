import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EquipmentManagerComponent } from './equipment-manager.component';

const routes: Routes = [{ path: '', component: EquipmentManagerComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EquipmentManagerRoutingModule { }
