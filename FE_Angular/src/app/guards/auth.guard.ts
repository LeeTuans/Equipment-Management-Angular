import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, map, of, catchError } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private _authService: AuthService, private _router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return true;
    if (route.data['page']) {
      return this._authService.checkLogin().pipe(
        map((response) => {
          if (response) {
            this._router.navigateByUrl('required');
            return false;
          }

          return true;
        }),
        catchError(() => {
          return of(true);
        })
      );
    }

    return this._authService.checkLogin().pipe(
      map((response) => {
        if (response) {
          return true;
        }

        this._router.navigateByUrl('login');
        return false;
      }),
      catchError(() => {
        this._router.navigateByUrl('login');
        return of(false);
      })
    );
  }
}
