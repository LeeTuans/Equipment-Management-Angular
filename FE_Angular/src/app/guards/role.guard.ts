import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, map, of, catchError } from 'rxjs';
import { UserService } from '../services/api/user.service';
import { AuthService } from '../services/auth.service';
import { listMenuBar } from '../dataListMenu/listMenuBar';

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  listMenu = listMenuBar;

  constructor(
    private _authService: AuthService,
    private _userService: UserService,
    private _router: Router
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return true;
    return this._authService.checkLogin().pipe(
      map((response) => {
        if (response) {
          let role = this.listMenu[0].items.find(
            (item) => item.link === `/${route.routeConfig?.path}`
          )?.role;
          if (role?.includes(this._userService.dataUser.Roles[0].RoleName))
            return true;
        }
        this._router.navigateByUrl('required');
        return false;
      }),
      catchError(() => {
        this._authService.logout();
        return of(false);
      })
    );
  }
}
