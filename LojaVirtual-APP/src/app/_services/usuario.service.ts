import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { UrlApi } from '../_utils/urlApi';

@Injectable({
  providedIn: 'root',
})
export class UsuarioService {
  constructor(private http: HttpClient) {}

  getAllUsuario(): Observable<User[]> {
    return this.http.get<User[]>(UrlApi.UrlUser);
  }

  getUserAuth(): Observable<User> {
    return this.http.get<User>(`${UrlApi.UrlUser}auth`);
  }

  getUsuarioById(id: number): Observable<User> {
    return this.http.get<User>(`${UrlApi.UrlUser}/${id}`);
  }

  postUsuario(user: User) {
    return this.http.post(UrlApi.UrlUser, user);
  }

  putUsuario(usuario: User) {
    return this.http.put(`${UrlApi.UrlUser}/${usuario.id}`, usuario);
  }

  deleteUsuario(id: number) {
    return this.http.delete(`${UrlApi.UrlUser}/${id}`);
  }

  postUpload(file: File, name: string) {
    const fileToUplaod = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUplaod, name);

    return this.http.post(`${UrlApi.UrlUser}upload`, formData);
  }
}
