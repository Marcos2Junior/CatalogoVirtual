import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Produto } from '../../_models/Produto';
import { ProdutoService } from '../../_services/produto.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CategoriaService } from '../../_services/categoria.service';
import { Categoria } from '../../_models/Categoria';
import { date } from '@rxweb/reactive-form-validators';
import { element } from 'protractor';


@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {

  produtos: Produto[];
  produto: Produto;
  categorias: Categoria[];
  categoriaForm: Categoria;
  selectDefaultCategoriaForm = true;
  categoriaSelect: Categoria;
  selectDefaultCategoriaSelect = true;
  produtoFiltro: Produto[];
  registerForm: FormGroup;
  modoSalvar: string;
  tituloModal: string;
  valorProduto: string;
  valorAntigo: string;
  defaultValue = '0';
  erroCategoria = false;
  _filtroString = '';
  constructor(
      private toastr: ToastrService
    , private produtoService: ProdutoService
    , private categoriaService: CategoriaService
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
    this.produtoFiltro = this.filtroLista || this.categoriaSelect ? this.filtrarProdutos(this._filtroString) : this.produtos;
  }

  filtrarProdutos(filtrarPorNome: string): Produto[] {
    let produtosFiltros = this.produtos;

    if (this.categoriaSelect) {
        produtosFiltros = this.produtos.filter(produto => produto.categoria.id === this.categoriaSelect.id);
    }

    if (filtrarPorNome) {
      filtrarPorNome = filtrarPorNome.toLocaleLowerCase();

      produtosFiltros = produtosFiltros.filter(
        produto => produto.nome.toLocaleLowerCase().indexOf(filtrarPorNome) !== -1);
    }

    return produtosFiltros;
  }
  openModal(template: any): void {
    this.registerForm.reset();
    this.tituloModal = this.modoSalvar === 'post' ? 'Novo Produto' : 'Editar Produto ID: ' + this.produto.id;
    template.show();
  }

  editarProduto(produto: Produto, template: any): void {
    this.modoSalvar = 'put';
    this.produto = Object.assign({}, produto);
    this.openModal(template);
    this.registerForm.patchValue(this.produto);
  }

  novoProduto(template: any): void {
    this.modoSalvar = 'post';
    this.onSelectCategoriaForm(0);
    this.erroCategoria = false;
    this.openModal(template);

    this.registerForm.patchValue(
      {
        estoque: 0,
        ativo: true,
        precoAtual: 0,
        precoAntigo: 0,
        descontoPorcentagem: 0
      });
  }

  salvarProduto(template: any): void{
    if (this.registerForm.valid && this.categoriaForm) {
      if (this.modoSalvar === 'post') {
        this.produto = Object.assign({ categoria: this.categoriaForm }, this.registerForm.value);
        this.produtoService.postProduto(this.produto).subscribe(
          (novoProduto: Produto) => {
            template.hide();
            this.getProdutos();
            this.toastr.success('Inserido com sucesso!');
          }, error => {
            this.toastr.error(`Erro ao inserir: ${error}`);
          }
        );
      }

      this.onSelectCategoriaForm(0);
    }
    else {
      this.registerForm.markAllAsTouched();

      if (!this.categoriaForm) {
        this.erroCategoria = true;
      }

      this.toastr.info('Por favor, verique se todos os campos estão preenchidos corretamente');
    }
  }

  validation(): void {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      descricao: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(400)]],
      precoAtual: ['', [Validators.required, Validators.min(0.01)]],
      precoAntigo: ['', [Validators.required]],
      descontoPorcentagem: ['', [Validators.required]],
      estoque: ['', [Validators.required, Validators.min(0), Validators.max(9999)]],
      ativo: ['']
    });
  }

  getProdutos(): void {
    this.produtoService.getAllProduto().subscribe(
      (Produtos: Produto[]) => {
        this.produtos = Produtos;
        this.produtoFiltro = Produtos;
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

  onSelectCategoriaForm(id: number): void {
    this.categoriaForm = this.categorias.find(x => x.id == id);

    if  (id === 0) {
      this.selectDefaultCategoriaForm = true;
    }
    else {
      this.selectDefaultCategoriaForm = false;
    }
    this.erroCategoria = this.categoriaForm ? false : true;
  }

  onSelectCategoriaFiltro(id: number): void {

    this.categoriaSelect = this.categorias.find(x => x.id == id);

    if  (id === 0) {
      this._filtroString = '';
      this.selectDefaultCategoriaSelect = true;
    }
    else {
      this.selectDefaultCategoriaSelect = false;
    }
    this.produtoFiltro = this.filtrarProdutos(this._filtroString);
  }
}
