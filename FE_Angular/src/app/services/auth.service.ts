import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, map, catchError } from 'rxjs';
import { tap } from 'rxjs/operators';
import { localStorageToken } from '../interface/interfaceData';
import { UserService } from './api/user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn: boolean = false;

  constructor(private _userService: UserService, private _router: Router) { }

  checkLogin(): Observable<boolean> {
    return this._userService.checkDataUser();
  }

  login(data: object) {
    return this._userService.login(data).pipe(
      tap((response) => {
        this.isLoggedIn = true;
        localStorage.setItem(localStorageToken, response.Token);
        this._userService.checkDataUser();
      })
    );
  }

  logout() {
    this.isLoggedIn = false;
    localStorage.removeItem('token');
    this._userService.dataUser = undefined;
    this._router.navigate(['/login']);
  }
}
