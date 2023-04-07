import { Component, ViewChild, Inject } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { EquipmentService } from 'src/app/services/api/equipment.service';
import { IEquipment } from 'src/app/interface/interfaceData';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'src/app/services/api/user.service';
import { EquipmentRequiredService } from 'src/app/services/api/required.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  displayedColumns: string[] = [];
  equipments!: IEquipment[];
  dataSource!: MatTableDataSource<IEquipment>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    public dialog: MatDialog,
    private _equipmentService: EquipmentService
  ) {
    this.refeshEquipment();
  }

  refeshEquipment() {
    this._equipmentService.getEquipments().subscribe((res) => {
      this.dataSource = new MatTableDataSource(res.Data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.equipments = createEuipmentPageData(this.dataSource);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    this.equipments = createEuipmentPageData(this.dataSource);

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  sortDataSource(id: string, start: string) {
    this.dataSource.data.sort((a: any, b: any) => {
      return a[id] > b[id] ? 1 : b[id] > a[id] ? -1 : 0;
    });
    this.dataSource._updateChangeSubscription();
  }

  getServerData(event: PageEvent) {
    this.equipments = createEuipmentPageData(this.dataSource);
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
  }

  openDialog(id: string): void {
    const dialogRef = this.dialog.open(DialogRequiredComponent, {
      data: { id },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.refeshEquipment();
      }
    });
  }
}

/** Builds and returns a new list. */
function createEuipmentPageData(dataSource: any): IEquipment[] {
  const page = dataSource.paginator?.pageIndex;
  const pageSize = dataSource.paginator?.pageSize;
  const pointStart = page * pageSize;

  let result: IEquipment[] = [];
  for (let i = pointStart; i < pointStart + pageSize; i++) {
    if (i < dataSource.filteredData.length)
      result.push(dataSource.filteredData[i]);
  }

  return result;
}

@Component({
  selector: 'app-dialog-required',
  template: `<div class="box-content">
    <h2 mat-dialog-title class="text-primary">
      <b> Requirement Equipment</b>
    </h2>
    <mat-dialog-content class="mat-typography m-2">
      Would you like to require this device?
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button [mat-dialog-close]="false">Cancel</button>
      <button mat-button (click)="save()" cdkFocusInitial>Submit</button>
    </mat-dialog-actions>
  </div>`,
})
export class DialogRequiredComponent {
  data = {
    EmployeeId: '',
    EquipmentId: '',
  };

  constructor(
    public dialogRef: MatDialogRef<DialogRequiredComponent>,
    private _userService: UserService,
    private _requiredService: EquipmentRequiredService,
    @Inject(MAT_DIALOG_DATA) public input: any
  ) {
    _userService.checkDataUser().subscribe((res) => {
      this.data.EmployeeId = _userService.dataUser.EmployeeId;
      this.data.EquipmentId = input.id;
    });
  }

  save(): void {
    this._requiredService.addRequire(this.data).subscribe(
      (res) => {
        Swal.fire(`Sent request success!!!`, '', 'success');
        this.dialogRef.close(true);
      },
      (err) => {
        Swal.fire(`What wrong???`, `Send request failed!!!`, 'error');
        this.dialogRef.close(false);
      }
    );
  }
}
