import { NgModule } from '@angular/core';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { MaterialModule } from './material/material.module';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { MenuBarComponent } from './components/menu-bar/menu-bar.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [NotFoundComponent, ToolbarComponent, HeaderComponent, MenuBarComponent],
  imports: [MaterialModule, RouterModule, CommonModule],
  exports: [MaterialModule, RouterModule, NotFoundComponent, ToolbarComponent, HeaderComponent, MenuBarComponent],
})
export class SharedModule { }
