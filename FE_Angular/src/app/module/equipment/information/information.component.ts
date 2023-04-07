import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { UserService } from 'src/app/services/api/user.service';
import { IUser } from 'src/app/interface/interfaceData';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-information',
  templateUrl: './information.component.html',
  styleUrls: ['./information.component.scss'],
})
export class InformationComponent {
  dataUser!: IUser;
  form = this._fb.group({
    Name: ['', [Validators.required]],
    Email: ['', [Validators.required, Validators.email]],
    Birthdate: [
      formatDate(Date.now(), 'yyyy-MM-dd', 'en'),
      [Validators.required],
    ],
    AvatarUrl: [''],
  });

  constructor(private _fb: FormBuilder, private _userService: UserService) {
    _userService.checkDataUser().subscribe((res) => {
      this.dataUser = _userService.dataUser;
      this.form = this._fb.group({
        Name: [this.dataUser.Name, [Validators.required]],
        Email: [
          { value: this.dataUser.Email, disabled: true },
          [Validators.required, Validators.email],
        ],
        Birthdate: [
          formatDate(this.dataUser.Birthdate, 'yyyy-MM-dd', 'en'),
          [Validators.required],
        ],
        AvatarUrl: [this.dataUser.AvatarUrl],
      });
    });
  }

  get f() {
    return this.form.controls;
  }

  onSubmit() {
    if (this.form.valid) {
      let data: any = this.form.value;
      data.ListRoles = [this.dataUser.Roles[0].RoleId];
      data.Email = this.f.Email.value;

      this._userService.updateUser(this.dataUser.EmployeeId, data).subscribe(
        (res) => {
          this._userService.refreshInformationUser().subscribe((res) => { });
          Swal.fire('Update profile success!!!', '', 'success');
        },
        (err) => {
          Swal.fire('What wrong??? Update profile failed!!!', '', 'error');
        }
      );
      return true;
    }
    return false;
  }
}
