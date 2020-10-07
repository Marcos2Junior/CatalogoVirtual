import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { KeysApp } from '../_utils/KeysApp';
import { UrlApi } from '../_utils/urlApi';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper = new JwtHelperService();
  decodedToken: any;

constructor(private http: HttpClient) { }

login(model: any) {
  return this.http
    .post(`${UrlApi.UrlUser}login`, model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem(KeysApp.localStorageJWT, user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          //sessionStorage.setItem('username', this.decodedToken.unique_name);
        }
      })
    );
}

register(model: any) {
  return this.http.post(`${UrlApi.UrlUser}register`, model);
}

loggedIn() {
  const token = localStorage.getItem('authAppCatalogoCris');
  return !this.jwtHelper.isTokenExpired(token);
}

}
