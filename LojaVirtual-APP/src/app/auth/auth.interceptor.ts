import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { KeysApp } from '../_utils/KeysApp';

@Injectable({providedIn: 'root'})
export class AuthInterceptor implements HttpInterceptor{

    constructor(private router: Router) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if(localStorage.getItem(KeysApp.localStorageJWT) !== null){
            const cloneReq = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${localStorage.getItem(KeysApp.localStorageJWT)}`)
            });
            return next.handle(cloneReq).pipe(
                tap(
                    succ => {},
                    err => {
                        if (err.status === 401){

                            if (localStorage.getItem(KeysApp.localStorageJWT) != null)
                            {
                                localStorage.removeItem(KeysApp.localStorageJWT);
                            }
                            this.router.navigateByUrl('user/login');
                        }
                    }
                )
            );
        }
        else{
            return next.handle(req.clone());
        }
    }
}
