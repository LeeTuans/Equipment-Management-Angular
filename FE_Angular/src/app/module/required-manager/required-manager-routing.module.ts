import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RequiredManagerComponent } from './required-manager.component';

const routes: Routes = [{ path: '', component: RequiredManagerComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RequiredManagerRoutingModule { }
