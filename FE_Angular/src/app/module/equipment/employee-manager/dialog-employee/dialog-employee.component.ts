import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'src/app/services/api/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dialog-employee',
  templateUrl: './dialog-employee.component.html',
  styleUrls: ['./dialog-employee.component.scss'],
})
export class DialogEmployeeComponent {
  form!: FormGroup;
  inputData: any;
  date = new Date();
  image = '';

  constructor(
    private _fb: FormBuilder,
    private _employeeService: UserService,
    public dialogRef: MatDialogRef<DialogEmployeeComponent>,
    @Inject(MAT_DIALOG_DATA) public input: any
  ) {
    this.inputData = input;
    if (this.inputData.data) this.image = this.inputData.data.AvatarUrl;
  }

  ngOnInit() {
    if (this.inputData.action !== 'DELETE')
      this.form = this._fb.group({
        ListRoles: [
          this.inputData.data
            ? this.inputData.data.Roles[0].RoleId.toString()
            : '1',
          Validators.required,
        ],
        Name: [
          this.inputData.data ? this.inputData.data.Name : '',
          [Validators.required],
        ],
        Email: [
          this.inputData.data ? this.inputData.data.Email : '',
          [Validators.required, Validators.email],
        ],
        Birthdate: [
          this.inputData.data ? this.inputData.data.Birthdate : this.date,
          [Validators.required],
        ],
        AvatarUrl: [this.inputData.data ? this.inputData.data.AvatarUrl : null],
      });
  }

  get f() {
    return this.form.controls;
  }

  save(): void {
    if (this.inputData.action === 'DELETE')
      this._employeeService
        .deleteUser(this.inputData.data.EmployeeId)
        .subscribe(this.showMessage('Create employee', true));

    if (this.form && this.form.valid) {
      switch (this.inputData.action) {
        case 'CREATE':
          this._employeeService
            .addUser(this.formatDataForm())
            .subscribe(this.showMessage('Create employee', true));
          break;
        case 'UPDATE':
          this._employeeService
            .updateUser(this.inputData.data.EmployeeId, this.formatDataForm())
            .subscribe(this.showMessage('Update employee', true));
          break;
      }
    } else {
      Swal.fire(
        'Please fill in correct and complete information!!!',
        '',
        'warning'
      );
    }
  }

  formatDataForm(): object {
    const data = this.form.value;
    data.ListRoles = [+data.ListRoles];
    console.log(data);

    return data;
  }

  showMessage(message: string, isCreateOrUpdate: boolean): object {
    const myObserver = {
      next: (res: any) => {
        Swal.fire(`${message} success!!!`, '', 'success');
        this.dialogRef.close(true);
      },
      error: (err: Error) => {
        if (isCreateOrUpdate) {
          console.log(
            this._employeeService
              .checkEmployee(this.form.value.Email)
              .subscribe((value) => {
                if (value) {
                  Swal.fire(`Email existed!!!`, '', 'warning');
                } else {
                  Swal.fire(`What wrong???`, `${message} failed!!!`, 'error');
                }
              })
          );
        } else {
          Swal.fire(`What wrong???`, `${message} failed!!!`, 'error');
          this.dialogRef.close(false);
        }
      },
    };

    return myObserver;
  }
}
