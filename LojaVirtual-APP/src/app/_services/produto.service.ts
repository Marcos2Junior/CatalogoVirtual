import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../_models/Produto';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {
  baseURL = 'http://localhost:53589/api/user/';

  constructor(private http: HttpClient) {}

  getAllProduto(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseURL);
  }
}
