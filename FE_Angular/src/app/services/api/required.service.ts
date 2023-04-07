import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class EquipmentRequiredService {
  listRequired!: any;
  baseURL = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  approveRequire(id: string) {
    const url = `${this.baseURL}/api/EquipmentHistories/Approve/${id}`;
    return this.http.put<any>(
      url,
      { EmployeeId: '1', EquipmentId: '1' }
    );
  }

  setReturnedDate(id: string) {
    const url = `${this.baseURL}/api/EquipmentHistories/CheckReturn/${id}`;
    return this.http.put<any>(
      url,
      { EmployeeId: '1', EquipmentId: '1' }
    );
  }

  // CRUD
  getEquipmentsRequired(id?: string) {
    if (id) {
      const url = `${this.baseURL}/api/EquipmentHistories/EmployeeId/${id}`;
      return this.http.get<any>(url);
    }
    const url = `${this.baseURL}/api/EquipmentHistories`;
    return this.http.get<any>(url);
  }

  getEquipmentsRequiredByEmployee(id: string) {
    const url = `${this.baseURL}/api/EquipmentHistories/EmployeeId/${id}`;
    return this.http.get<any>(url);
  }

  addRequire(data: object) {
    const url = `${this.baseURL}/api/EquipmentHistories`;
    return this.http.post<any>(url, data);
  }

  // updateRequire(id: string, data: object) {
  //   const url = `${this.baseURL}/api/EquipmentHistories/${id}`;
  //   return this.http.put<any>(url, data);
  // }

  // deleteRequire(id: string) {
  //   const url = `${this.baseURL}/api/EquipmentHistories/${id}`;
  //   return this.http.delete<any>(url);
  // }
}
