
import { Component, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { IEquipmentRequired, IUser } from 'src/app/interface/interfaceData';
import { EquipmentRequiredService } from 'src/app/services/api/required.service';
import { UserService } from 'src/app/services/api/user.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { DialogRequiredComponent } from './dialog-required/dialog-required.component';

@Component({
  selector: 'app-required-manager',
  templateUrl: './required-manager.component.html',
  styleUrls: ['./required-manager.component.scss']
})
export class RequiredManagerComponent {
  ngxuiloader = {
    bgsColor: '#696cff',
    bgsOpacity: 0.8,
    bgsPosition: 'top-center' as const,
    bgsSize: 50,
    bgsType: 'three-bounce' as const,
    blur: 5,
    delay: 0,
    fastFadeOut: true,
    fgsColor: '#349cdd',
    fgsPosition: 'center-center' as const,
    fgsSize: 60,
    fgsType: 'rotating-plane' as const,
    gap: 24,
    logoPosition: 'center-center' as const,
    logoSize: 120,
    logoUrl: '',
    masterLoaderId: 'master',
    overlayBorderRadius: '0',
    overlayColor: 'rgba(40, 40, 40, 0.8)',
    pbColor: 'red',
    pbDirection: 'ltr' as const,
    pbThickness: 3,
    hasProgressBar: true,
    text: '',
    textColor: '#FFFFFF',
    textPosition: 'center-center' as const,
    maxTime: -1,
    minTime: 300,
  };
  displayedColumns: string[] = [
    'id',
    'Equipment',
    'Employee',
    'Approved',
    'BorrowedDate',
    'ReturnedDate',
  ];
  requirements!: IEquipmentRequired[];
  dataSource!: MatTableDataSource<any>;
  dataUser!: IUser;
  isAdmin!: boolean;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    public dialog: MatDialog,
    private _requiredService: EquipmentRequiredService,
    private _userService: UserService,
    private ngxService: NgxUiLoaderService
  ) {
    _userService.checkDataUser().subscribe(
      (res) => {
        this.dataUser = _userService.dataUser;
        this.isAdmin = this.dataUser.Roles[0].RoleName === 'Admin';

        if (this.isAdmin) {
          this.displayedColumns.push('Action');
          this.refeshEquipment();
        } else {
          this.refeshEquipment(this.dataUser.EmployeeId);
        }
      },
      (err) => { }
    );
  }

  ngOnInit() {
    this.ngxService.startBackground();
  }

  refeshEquipment(id?: string) {
    this._requiredService.getEquipmentsRequired(id).subscribe((res) => {
      this.ngxService.stopBackground();
      setTimeout(() => {
        this.requirements = createEuipmentData(res.Data);
        this.dataSource = new MatTableDataSource(this.requirements);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, 400);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog(action: string, id?: number): void {
    let dataItem = id != null ? this.requirements[id - 1] : null;
    const dialogRef = this.dialog.open(DialogRequiredComponent, {
      data: { action: action, data: dataItem },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.refeshEquipment();
      }
    });
  }
}

/** Builds and returns a new list. */
function createEuipmentData(data: any[]): IEquipmentRequired[] {
  let i = 1;
  data.sort((a, b) =>
    b.BorrowedDate > a.BorrowedDate
      ? 1
      : a.BorrowedDate > b.BorrowedDate
        ? -1
        : 0
  );
  data.forEach((item) => {
    item.IsAvailable = item.Equipment.IsAvailable;
    item.Equipment = item.Equipment?.Name;
    item.Employee = item.Employee?.Name;
    item.Approved = item.IsAproved
      ? 'Approved'
      : item.IsAvailable
        ? 'Waiting'
        : 'Out of stock';

    item.id = i;
    i++;
  });

  return data;
}
