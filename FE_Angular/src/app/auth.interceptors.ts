import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, filter, take, switchMap, finalize } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
  ) { }

  public intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');
    if (token !== null) {
      const headers = new HttpHeaders()
        .set('access-token', token)
        .set('Authorization', `Bearer ${token}`,)
      const AuthRequest = request.clone({ headers: headers })
      return next.handle(AuthRequest);
    } else {
      return next.handle(request)
    }
  }
}




