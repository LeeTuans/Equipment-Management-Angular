import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class EquipmentService {
  listEquipment!: any;
  baseURL = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  getEquipments() {
    const url = `${this.baseURL}/api/Equipments`;
    return this.http.get<any>(url);
  }

  addEquipment(data: object) {
    const url = `${this.baseURL}/api/Equipments`;
    return this.http.post<any>(url, data);
  }

  updateEquipment(id: string, data: object) {
    const url = `${this.baseURL}/api/Equipments/${id}`;
    return this.http.put<any>(url, data);
  }

  deleteEquipment(id: string) {
    const url = `${this.baseURL}/api/Equipments/${id}`;
    return this.http.delete<any>(url);
  }
}
