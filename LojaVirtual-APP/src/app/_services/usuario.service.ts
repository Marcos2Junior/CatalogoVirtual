import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

baseURL = 'https://localhost:5001/api/user';

  constructor(private http: HttpClient) { }

  getAllUsuario(): Observable<User[]> {
    return this.http.get<User[]>(this.baseURL);
  }

  getUsuarioById(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseURL}/${id}`);
  }

  postUsuario(user: User){
    return this.http.post(this.baseURL, user);
  }

  putUsuario(usuario: User) {
    return this.http.put(`${this.baseURL}/${usuario.id}`, usuario);
  }

  deleteUsuario(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
