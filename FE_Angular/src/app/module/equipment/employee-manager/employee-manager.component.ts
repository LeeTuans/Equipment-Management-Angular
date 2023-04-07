import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { OptionDataService } from 'src/app/services/api/optionData.service';
import { IEmployee, IRole, IUser } from 'src/app/interface/interfaceData';
import { UserService } from 'src/app/services/api/user.service';
import { DialogEmployeeComponent } from './dialog-employee/dialog-employee.component';

@Component({
  selector: 'app-employee-manager',
  templateUrl: './employee-manager.component.html',
  styleUrls: ['./employee-manager.component.scss'],
})
export class EmployeeManagerComponent {
  displayedColumns: string[] = [
    'id',
    'Name',
    'Email',
    'Birthdate',
    'Role',
    'Action',
  ];
  employees!: IEmployee[];
  dataUser!: IUser;
  listRoles!: IRole[];
  dataSource!: MatTableDataSource<IEmployee>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    public dialog: MatDialog,
    private _employeeService: UserService,
    private _roleService: OptionDataService,
    private _userService: UserService
  ) {
    _roleService.getRoles().subscribe(
      (res) => {
        this.dataUser = _userService.dataUser;
        this.listRoles = res.Data;
        this.refeshEmployee();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  refeshEmployee(): void {
    this._employeeService.getEmployees().subscribe((res) => {
      this.employees = createEuipmentData(res.Data);
      this.dataSource = new MatTableDataSource(this.employees);
      this.dataSource.filterPredicate = function (record, filter) {
        return (
          record.Name.toLocaleLowerCase().includes(
            filter.toLocaleLowerCase()
          ) ||
          record.Email.toLocaleLowerCase().includes(filter.toLocaleLowerCase())
        );
      };
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog(action: string, id?: number): void {
    let dataItem = id != null ? this.employees[id - 1] : null;
    const dialogRef = this.dialog.open(DialogEmployeeComponent, {
      data: { action: action, data: dataItem, listRoles: this.listRoles },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.refeshEmployee();
      }
    });
  }
}

/** Builds and returns a new list. */
function createEuipmentData(data: any[]): IEmployee[] {
  let i = 1;
  data.sort((a, b) =>
    a.Roles[0].RoleName > b.Roles[0].RoleName
      ? 1
      : b.Roles[0].RoleName > a.Roles[0].RoleName
        ? -1
        : 0
  );
  data.forEach((item) => {
    item.id = i;
    i++;
  });

  return data;
}
