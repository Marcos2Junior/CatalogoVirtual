import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categoria } from '../_models/Categoria';
import { UrlApi } from '../_utils/urlApi';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

constructor(private http: HttpClient) { }

getCategorias(): Observable<Categoria[]>{
  return this.http.get<Categoria[]>(UrlApi.UrlCategoria);
}

}
