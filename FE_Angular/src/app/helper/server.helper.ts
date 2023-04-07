import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

export class ServerHelper {
  static handleError<T>(operation = 'operation') {
    return (error: HttpErrorResponse): Observable<T> => {
      const message = `server return code ${error.status} with body "${error.error}"`;
      throw new Error(`${operation} faild: ${message}`);
    };
  }
}
