import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';
import { EquipmentService } from 'src/app/services/api/equipment.service';
import { OptionDataService } from 'src/app/services/api/optionData.service';
import { IEquipment, IEquipmentType } from 'src/app/interface/interfaceData';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.scss'],
})
export class ManagerComponent {
  displayedColumns: string[] = [
    'id',
    'Name',
    'IsAvailable',
    'Description',
    'EquipmentType',
    'Action',
  ];
  equipments!: IEquipment[];
  equipmentTypes!: IEquipmentType[];
  dataSource!: MatTableDataSource<IEquipment>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    public dialog: MatDialog,
    private _equipmentService: EquipmentService,
    private _optionService: OptionDataService
  ) {
    _optionService.getEquipmentTypes().subscribe((types) => {
      this.equipmentTypes = types.Data;
      this.refeshEquipment();
    });
  }

  refeshEquipment(): void {
    this._equipmentService.getEquipments().subscribe((res) => {
      this.equipments = createEuipmentData(res.Data);
      this.dataSource = new MatTableDataSource(this.equipments);
      this.dataSource.filterPredicate = function (record, filter) {
        return (
          record.Name.toLocaleLowerCase().includes(
            filter.toLocaleLowerCase()
          ) ||
          record.Description.toLocaleLowerCase().includes(
            filter.toLocaleLowerCase()
          )
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
    let dataItem = id != null ? this.equipments[id - 1] : null;
    const dialogRef = this.dialog.open(DialogComponent, {
      data: { action: action, data: dataItem, dataType: this.equipmentTypes },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.refeshEquipment();
      }
    });
  }
}

/** Builds and returns a new list. */
function createEuipmentData(data: any[]): IEquipment[] {
  let i = 1;
  data.forEach((item) => {
    item.id = i;
    i++;
  });
  return data;
}
