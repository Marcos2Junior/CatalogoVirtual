import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../_models/Produto';
import { UrlApi } from '../_utils/urlApi';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {

  constructor(private http: HttpClient) {}

  getAllProduto(): Observable<Produto[]> {
    return this.http.get<Produto[]>(UrlApi.UrlProduto);
  }

  postProduto(produto: Produto) {
    return this.http.post(UrlApi.UrlProduto, produto);
  }

  deleteProduto(produto: Produto) {
    return this.http.delete(UrlApi.UrlProduto + produto.id);
  }

  putProduto(produto: Produto) {
    return this.http.put(UrlApi.UrlProduto + produto.id, produto);
  }
}
