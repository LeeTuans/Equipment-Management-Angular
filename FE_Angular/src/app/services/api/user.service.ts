import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap, map } from 'rxjs';
import { IUser, localStorageToken } from 'src/app/interface/interfaceData';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  dataUser!: any;
  listEmployee: any;
  baseURL = environment.apiUrl;

  constructor(
    private _http: HttpClient
  ) { }

  refreshInformationUser(): Observable<any> {
    const url = `${this.baseURL}/api/GetCurrentInforEmployee`;
    return this._http.get<any>(url).pipe(
      tap((res) => {
        this.dataUser = res.Data;
      })
    );
  }

  checkDataUser(): Observable<any> {
    if (!this.dataUser) {
      if (!!localStorage.getItem(localStorageToken))
        return this.refreshInformationUser();
      return of(false);
    }
    return of(true);
  }

  checkEmployee(email: string): Observable<boolean> {
    return this.getEmployees().pipe(
      map(
        (res) => {
          if (res) {
            for (let i = 0; i < res.Data.length; i++) {
              if (res.Data[i].Email === email) return true;
            }
          }

          return false;
        },
        catchError((e) => of(true))
      )
    );
  }

  login(data: object): Observable<any> {
    const url = `${this.baseURL}/api/Login`;
    return this._http.post<any>(url, data);
  }

  changePassword(id: string, data: object): Observable<any> {
    const url = `${this.baseURL}/api/Employees/ChangePassword/${id}`;
    return this._http.put<any>(url, data);
  }

  // CRUD User
  getEmployees(): Observable<any> {
    const url = `${this.baseURL}/api/Employees`;
    return this._http.get<any>(url);
  }

  addUser(data: any): Observable<any> {
    const url = `${this.baseURL}/api/Employees`;
    return this._http.post<any>(url, data);
  }

  updateUser(id: string, data: object): Observable<any> {
    const url = `${this.baseURL}/api/Employees/${id}`;
    return this._http.put<any>(url, data);
  }

  deleteUser(id: string): Observable<any> {
    const url = `${this.baseURL}/api/Employees/${id}`;
    return this._http.delete<any>(url);
  }
}
