import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';
import { UserService } from 'src/app/services/api/user.service';
import { IUser } from 'src/app/interface/interfaceData';
import Swal from 'sweetalert2';
import { ValidateHelper } from 'src/app/helper/validate.helper';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent {
  dataUser!: IUser;
  form!: FormGroup;
  submitted: boolean = false;

  constructor(private _fb: FormBuilder, private _userService: UserService) {
    _userService.checkDataUser().subscribe((res) => {
      this.dataUser = _userService.dataUser;
    });
  }

  ngOnInit() {
    this.form = this._fb.group({
      Password: ['', [Validators.required]],
      NewPassword: [
        '',
        [
          Validators.required,
          Validators.pattern(
            /(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^_-]).{8,}/
          ),
          ValidateHelper.isContainSpace,
        ],
      ],
      NewPasswordConfirmed: [''],
    });
  }

  get f() {
    return this.form.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.valid) {
      this.f['NewPasswordConfirmed'].setValue(this.f['NewPassword'].value);
      this._userService
        .changePassword(this.dataUser.EmployeeId, this.form.value)
        .subscribe(
          (res) => {
            Swal.fire('Change password success!!!', '', 'success');
          },
          (err) => {
            Swal.fire('What wrong??? Change password failed!!!', '', 'error');
          }
        );
      return;
    }
  }
}
