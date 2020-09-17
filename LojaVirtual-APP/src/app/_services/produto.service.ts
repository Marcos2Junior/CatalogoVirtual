import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  baseURL = 'https://localhost:5001/api/produto';

constructor(private http: HttpClient) { }

getAllProduto(){
  
}

}
