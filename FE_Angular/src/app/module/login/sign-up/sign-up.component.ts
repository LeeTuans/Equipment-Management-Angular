import { Component } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidateHelper } from 'src/app/helper/validate.helper';
import { UserService } from 'src/app/services/api/user.service';
import { IRole } from 'src/app/interface/interfaceData';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent {
  submitted = false;
  listRoles: IRole[] = [
    { RoleId: '1', RoleName: 'User' },
    { RoleId: '2', RoleName: 'Admin' },
  ];

  form = this._fb.group({
    ListRoles: ['1', Validators.required],
    Name: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    Password: [
      '',
      [
        Validators.required,
        Validators.pattern(
          /(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^_-]).{8,}/
        ),
        ValidateHelper.isContainSpace,
      ],
    ],
  });

  constructor(
    private _fb: FormBuilder,
    private _router: Router,
    private _userService: UserService
  ) { }

  get f() {
    return this.form.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.valid) {
      this._userService.addUser(this.form.value).subscribe(
        () => {
          Swal.fire('Sign Up User Success!', '', 'success');
          this._router.navigate(['/login']);
        },
        (error) => { }
      );
    }
  }
}
