import { Component, OnInit } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Produto } from '../../_models/Produto';
import { ProdutoService } from '../../_services/produto.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CategoriaService } from '../../_services/categoria.service';
import { Categoria } from '../../_models/Categoria';


@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {

  produtos: Produto[];
  produto: Produto;
  categorias: Categoria[];
  categoria: Categoria;
  produtoFiltro: Produto[];
  registerForm: FormGroup;
  modoSalvar: string;
  tituloModal: string;
  valorProduto: string;
  valorAntigo: string;
  _filtroString = '';
  constructor(
      private toastr: ToastrService
    , private produtoService: ProdutoService
    , private categoriaService: CategoriaService
    , private modalService: BsModalService
    , private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.validation();
    this.getProdutos();
    this.getCategorias();
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
    this.tituloModal = this.modoSalvar === 'post' ? 'Novo Produto' : 'Editar Produto ID: ' + this.produto.id;
    template.show();
  }

  editarProduto(produto: Produto, template: any) {
    this.modoSalvar = 'put';
    this.produto = Object.assign({}, produto);
    this.openModal(template);
    this.registerForm.patchValue(this.produto);
  }

  novoProduto(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  salvarProduto(template: any){
    console.log(this.registerForm.valid);
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.produto = Object.assign({ categoria: this.categoria }, this.registerForm.value);
        console.log(this.produto);
        this.produtoService.postProduto(this.produto).subscribe(
          (novoProduto: Produto) => {
            template.hide();
            this.getProdutos();
            this.toastr.success('Inserido com sucesso!');
          }, error => {
            console.log(error);
            this.toastr.error(`Erro ao inserir: ${error}`);
          }
        );
      }
    }
  }

  validation() {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      descricao: ['', [Validators.required, Validators.maxLength(500)]],
      precoAtual: [],
      precoAntigo: [],
      descontoPorcentagem: ['', [Validators.min(1), Validators.max(100)]],
      estoque: ['', [Validators.min(0), Validators.max(9999)]],
      destaque: [],
      ativo: [true],
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

  getCategorias(): void{
    this.categoriaService.getCategorias().subscribe(
      (categorias: Categoria[]) => {
        this.categorias = categorias;
      }, error => {
        this.toastr.error('Ops! Não foi possível carregar as categorias');
      }
    );
  }

  onSelectCategoria(id: number) {
    this.categoria = this.categorias.find(x => x.id == id);
  }
}
