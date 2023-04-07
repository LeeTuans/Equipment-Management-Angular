import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EquipmentRequiredService } from 'src/app/services/api/required.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dialog-required',
  templateUrl: './dialog-required.component.html',
  styleUrls: ['./dialog-required.component.scss']
})
export class DialogRequiredComponent {
  inputData: any;
  date: Date = new Date();

  constructor(
    private _requiredService: EquipmentRequiredService,
    public dialogRef: MatDialogRef<DialogRequiredComponent>,
    @Inject(MAT_DIALOG_DATA) public input: any
  ) {
    this.inputData = input;
  }

  save(): void {
    switch (this.inputData.action) {
      case 'APPROVAL':
        this._requiredService
          .approveRequire(this.inputData.data.EquipmentHistoryId)
          .subscribe(this.showMessage('Change approval status', false));
        break;
      case 'RETURNED_DATE':
        console.log();

        this._requiredService
          .setReturnedDate(this.inputData.data.EquipmentHistoryId)
          .subscribe(this.showMessage('Set returned date', false));
        break;
    }
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
