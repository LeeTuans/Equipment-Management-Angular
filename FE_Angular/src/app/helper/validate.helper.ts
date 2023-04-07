import { AbstractControl, ValidationErrors } from '@angular/forms';

export class ValidateHelper {
  static isEmptyString(operation = '') {
    return operation.trim().length === 0;
  }

  static isContainSpace(control: AbstractControl): ValidationErrors | null {
    if ((control.value as string).indexOf(' ') >= 0) {
      return { cannotContainSpace: true };
    }

    return null;
  }

  static isConfirmedPassword(control: any): ValidationErrors | null {
    const newPassword: string = control.value.NewPassword;
    const confirmPassword: string = control.value.NewPasswordConfirmed;

    if (newPassword !== confirmPassword) {
      control.controls['NewPasswordConfirmed'].setErrors({ mismatch: true });
    }
    return null;
  }
}
