import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/api/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { listMenuBar } from 'src/app/dataListMenu/listMenuBar';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.scss'],
})
export class EquipmentComponent {
  listMenu = listMenuBar;
  menuItem: string = 'Home';
  isToggle: boolean = false;

  constructor(
    private _router: Router,
    private _authService: AuthService,
    public userService: UserService
  ) {
    this.userService.checkDataUser();
  }

  changeMenuItem(input: string) {
    if (input === 'Log out') {
      this.logOut();
    }

    for (let i = 0; i < this.listMenu.length; i++) {
      let itemLink = this.listMenu[i].items.find(
        (item) => item.title.toLocaleLowerCase() === input.toLocaleLowerCase()
      )?.link;

      if (itemLink) {
        this._router.navigate([itemLink]);
        break;
      }
    }
  }

  toggleMenu(input: boolean) {
    this.isToggle = input;
  }

  logOut() {
    this._authService.logout();
  }
}
