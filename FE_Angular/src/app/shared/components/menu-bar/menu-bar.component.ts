import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { listMenuBar } from 'src/app/dataListMenu/listMenuBar';
import { IUser } from 'src/app/interface/interfaceData';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.scss'],
})
export class MenuBarComponent {
  @Input() menuItem: string = '';
  @Input() isToggle: boolean = false;
  @Input() role: string = 'Admin';
  @Output() changeMenuItem = new EventEmitter<string>();
  @Output() toggleMenu = new EventEmitter<boolean>();


  listMenu = listMenuBar;

  constructor(private _router: Router) {
  }

  ngOnInit() {
    if (this._router.url === '/') {
      this.menuItem = 'Home';
      return;
    }
    for (let i = 0; i < this.listMenu.length; i++) {
      let itemLink = this.listMenu[i].items.find(
        (item) => item.link === this._router.url
      )?.title;

      if (itemLink) {
        this.menuItem = itemLink;
        break;
      }
    }
  }

  // Update At Home
  checkPermission(input: string[]): boolean {
    const roleData = input.find(
      (index) =>
        index === this.role
    );
    if (roleData === undefined) return false;

    return true;
  }

  clickItem(input: string): void {
    if (input !== this.menuItem) {
      this.menuItem = input;
      this.changeMenuItem.emit(input);
      this.onToggle()
    }
  }

  onToggle(): void {
    this.toggleMenu.emit(!this.isToggle);
  }
}
