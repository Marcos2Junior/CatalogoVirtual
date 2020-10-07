import { Component, OnInit } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Produto } from '../_models/Produto';
import { ProdutoService } from '../_services/produto.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {

  produtos: Produto[];
  produto: Produto;
  produtoFiltro: Produto[];
  registerForm: FormGroup;
  _filtroString = '';
  constructor(
      private toastr: ToastrService
    , private produtoService: ProdutoService
    , private modalService: BsModalService
    , private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.validation();
    this.getProdutos();
  }

  get filtroLista(): string {
    return this._filtroString;
  }
  set filtroLista(value: string) {
    this._filtroString = value;
    this.produtoFiltro = this.filtroLista ? this.filtrarProdutos(this._filtroString) : this.produtos;

  }

  filtrarProdutos(filtrarPor: string): Produto[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.produtos.filter(
      produto => produto.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }
  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  editarProduto(produto: Produto, template: any) {
    this.openModal(template);
    this.produto = Object.assign({}, produto);
    console.log(this.produto);
    this.registerForm.patchValue(this.produto);
  }

  novoProduto(template: any) {
    this.openModal(template);
  }

  validation() {
    this.registerForm = this.fb.group({
      id: ['', [Validators.nullValidator]],
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      descricao: ['', [Validators.required, Validators.maxLength(500)]],
      precoAtual: ['', [Validators.required, Validators.min(0), Validators.max(9999)]],
      precoAntigo: ['', [Validators.min(0), Validators.max(100)]],
      descontoPorcentagem: ['', [Validators.min(0), Validators.max(100)]],
      estoque: ['0', Validators.required],
      categoria: ['', Validators.required],
      destaque: [],
      ativo: ['', Validators.required],
    });
  }

  getProdutos(): void {
    this.produtoService.getAllProduto().subscribe(
      (Produtos: Produto[]) => {
        this.produtos = Produtos;
        console.log(this.produtos);
      }, error => {
        this.toastr.error(`Erro ao tentar carregar os produtos: ${error}`);
      }
    );
  }
}
