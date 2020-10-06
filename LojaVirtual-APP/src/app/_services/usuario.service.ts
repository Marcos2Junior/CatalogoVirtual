import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root',
})
export class UsuarioService {
  baseURL = 'http://localhost:53589/api/user/';

  constructor(private http: HttpClient) {}

  getAllUsuario(): Observable<User[]> {
    return this.http.get<User[]>(this.baseURL);
  }

  getUserAuth(): Observable<User> {
    return this.http.get<User>(`${this.baseURL}auth`);
  }

  getUsuarioById(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseURL}/${id}`);
  }

  postUsuario(user: User) {
    return this.http.post(this.baseURL, user);
  }

  putUsuario(usuario: User) {
    return this.http.put(`${this.baseURL}/${usuario.id}`, usuario);
  }

  deleteUsuario(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  postUpload(file: File, name: string) {
    const fileToUplaod = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUplaod, name);

    return this.http.post(`${this.baseURL}upload`, formData);
  }
}
