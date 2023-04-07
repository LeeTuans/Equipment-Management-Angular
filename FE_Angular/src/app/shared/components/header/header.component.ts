import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserService } from 'src/app/services/api/user.service';
import { IUser } from 'src/app/interface/interfaceData';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  @Input() menuItem: string = '';
  @Output() changeMenuItem = new EventEmitter<string>();

  @Input() isToggle: boolean = false;
  @Output() toggleMenu = new EventEmitter<boolean>();

  dataUser!: IUser;
  isActive: boolean = false;

  constructor(private _userService: UserService) {
    _userService.checkDataUser().subscribe((res) => {
      this.dataUser = _userService.dataUser;
    });
  }

  onToggle() {
    this.toggleMenu.emit(!this.isToggle);
  }

  clickItem(input: string) {
    if (input !== this.menuItem) this.changeMenuItem.emit(input);
  }
}
