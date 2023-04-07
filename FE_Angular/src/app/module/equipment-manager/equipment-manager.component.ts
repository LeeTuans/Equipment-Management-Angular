
import { Component, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { EquipmentService } from 'src/app/services/api/equipment.service';
import { OptionDataService } from 'src/app/services/api/optionData.service';
import { IEquipment, IEquipmentType } from 'src/app/interface/interfaceData';
import { DialogEquipmentComponent } from './dialog-equipment/dialog-equipment.component';
import { SortDirective, SortEvent, compare } from './directive/sort.directive';

type TEquipment = IEquipment & {
  id: number
}

@Component({
  selector: 'app-equipment-manager',
  templateUrl: './equipment-manager.component.html',
  styleUrls: ['./equipment-manager.component.scss']
})
export class EquipmentManagerComponent {
  displayedColumns: string[] = [
    'id',
    'Name',
    'IsAvailable',
    'Description',
    'EquipmentType',
    'Action',
  ];
  equipments!: TEquipment[];
  equipmentTypes!: IEquipmentType[];
  equipmentsShow!: TEquipment[];
  paginationData: any = { previousPageIndex: 0, pageIndex: 0, pageSize: 10, length: 0 }
  filterValue: string = '';
  @ViewChildren(SortDirective) headers!: QueryList<SortDirective>;

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
      this.equipmentsShow = [...this.equipments];
      this.paginationData = { previousPageIndex: 0, pageIndex: 0, pageSize: 10, length: 0 }
    });
  }

  public getServerData(event?: PageEvent) {
    this.paginationData = event;
  }

  onSort({ column, direction }: SortEvent) {
    this.headers.forEach((header) => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });

    // sorting countries
    if (direction === '' || column === '') {
      this.equipmentsShow = this.equipments;
    } else {
      this.equipmentsShow = [...this.equipments].sort((a: any, b: any) => {
        const res = compare(a[column], b[column]);
        return direction === 'asc' ? res : -res;
      });
    }
  }

  openDialog(action: string, id?: number): void {
    let dataItem = id != null ? this.equipments[id - 1] : null;
    const dialogRef = this.dialog.open(DialogEquipmentComponent, {
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
function createEuipmentData(data: any[]): TEquipment[] {
  let i = 1;
  data.forEach((item) => {
    item.id = i;
    i++;
  });
  return data;
}