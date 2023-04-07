import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { IRole } from 'src/app/interface/interfaceData';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class OptionDataService {
  listRole!: IRole[];
  listEquipmentType!: any;
  baseURL = environment.apiUrl;

  constructor(
    private _http: HttpClient
  ) { }

  // Role User
  getRoles(): Observable<any> {
    const url = `${this.baseURL}/api/Roles`;
    return this._http.get<any>(url).pipe(
      tap((response) => {
        this.listRole = response.Data;
      })
    );
  }

  // =====================
  addRole(data: object) {
    const url = `${this.baseURL}/roles/add`;
    return this._http.post<any>(url, data);
  }

  updateRole(id: string, data: object) {
    const url = `${this.baseURL}/roles/update?id=${id}`;
    return this._http.put<any>(url, data);
  }

  deleteRole(id: string) {
    const url = `${this.baseURL}/roles/delete?id=${id}`;
    return this._http.delete<any>(url);
  }

  // Equipment Type
  getEquipmentTypes() {
    const url = `${this.baseURL}/api/EquipmentTypes`;
    return this._http.get<any>(url);
  }

  // ===================
  addEquipmentType(data: object) {
    const url = `${this.baseURL}/api/EquipmentTypes`;
    return this._http.post<any>(url, data);
  }

  updateEquipmentType(id: string, data: object) {
    const url = `${this.baseURL}/api/EquipmentTypes/${id}`;
    return this._http.put<any>(url, data);
  }

  deleteEquipmentType(id: string) {
    const url = `${this.baseURL}/api/EquipmentTypes/${id}`;
    return this._http.delete<any>(url);
  }
}
