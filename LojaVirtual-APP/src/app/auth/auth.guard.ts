import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { KeysApp } from '../_utils/KeysApp';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean  {

      if (localStorage.getItem(KeysApp.localStorageJWT) !== null)
      {
        return true;
      }
      else
      {
        this.router.navigate(['/user/login']);
        return false;
      }
  }

}
