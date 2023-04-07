
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EquipmentService } from 'src/app/services/api/equipment.service';
import { EquipmentRequiredService } from 'src/app/services/api/required.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dialog-equipment',
  templateUrl: './dialog-equipment.component.html',
  styleUrls: ['./dialog-equipment.component.scss']
})
export class DialogEquipmentComponent {
  form!: FormGroup;
  inputData: any;
  image = '';

  constructor(
    private _fb: FormBuilder,
    private _equipmentService: EquipmentService,
    private _requiredService: EquipmentRequiredService,
    public dialogRef: MatDialogRef<DialogEquipmentComponent>,
    @Inject(MAT_DIALOG_DATA) public input: any
  ) {
    this.inputData = input;
    if (this.inputData.data) this.image = this.inputData.data.ImageUrl;
  }

  ngOnInit() {
    if (
      this.inputData.action === 'CREATE' ||
      this.inputData.action === 'UPDATE'
    )
      this.form = this._fb.group({
        Name: [
          this.inputData.data ? this.inputData.data.Name : '',
          [Validators.required],
        ],
        EquipmentTypeId: [
          this.inputData.data
            ? this.inputData.data.EquipmentType.EquipmentTypeId.toString()
            : this.inputData.dataType ? this.inputData.dataType[0].EquipmentTypeId.toString() : '',
          [Validators.required],
        ],
        IsAvailable: [
          this.inputData.data
            ? this.inputData.data.IsAvailable.toString()
            : 'true',
          [Validators.required],
        ],
        Description: [
          this.inputData.data ? this.inputData.data.Description : '',
          [Validators.required],
        ],
        ImageUrl: [this.inputData.data ? this.inputData.data.ImageUrl : ''],
      });
  }

  save(): void {
    if (this.inputData.action === 'DELETE') {
      this._equipmentService
        .deleteEquipment(this.inputData.data.EquipmentId)
        .subscribe(this.showMessage('Delete equipment', false));
    }

    if (this.form && this.form.valid) {
      switch (this.inputData.action) {
        case 'CREATE':
          this._equipmentService
            .addEquipment(this.formatDataForm())
            .subscribe(this.showMessage('Create equipment', true));
          break;
        case 'UPDATE':
          this._equipmentService
            .updateEquipment(
              this.inputData.data.EquipmentId,
              this.formatDataForm()
            )
            .subscribe(this.showMessage('Update equipment', true));
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
    data.IsAvailable = data.IsAvailable === 'true';
    data.ImageUrl =
      data.ImageUrl.trim() === ''
        ? 'https://media.gcflearnfree.org/content/55e0730c7dd48174331f5164_01_17_2014/whatisacomputer_pc.jpg'
        : data.ImageUrl.trim();
    return data;
  }

  showMessage(message: string, isCreateOrUpdate: boolean): object {
    const myObserver = {
      next: (res: any) => {
        Swal.fire(`${message} success!!!`, '', 'success');
        this.dialogRef.close(true);
      },
      error: (err: Error) => {
        Swal.fire(`What wrong???`, `${message} failed!!!`, 'error');
        if (!isCreateOrUpdate) this.dialogRef.close(false);
      },
    };
    return myObserver;
  }
}
