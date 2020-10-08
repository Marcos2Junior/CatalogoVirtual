import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Categoria } from 'src/app/_models/Categoria';
import { CategoriaService } from 'src/app/_services/categoria.service';

@Component({
  selector: 'app-categoria',
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.css']
})
export class CategoriaComponent implements OnInit {

  registerForm: FormGroup;
  categoria: Categoria;
  categorias: Categoria[];
  tipoModal: string;

  constructor(
    private categoriaService: CategoriaService
  , private toastr: ToastrService
  , private modalService: BsModalService
  , private fb: FormBuilder
  ) { }


  ngOnInit() {
    this.validation();
    this.getCategorias();
  }

  openModal(template: any, tipoModal: string): void {
    this.tipoModal = tipoModal;
    this.registerForm.reset();
    template.show();
  }

  getCategorias(): void {
    this.categoriaService.getCategorias().subscribe(
      (categoria: Categoria[]) => {
        this.categorias = categoria;
      }, error => {
        this.toastr.error(`Erro ao tentar carregar categorias: ${error}`);
      });
  }

  validation(): void {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(40)]],
      descricao: ['', Validators.maxLength(2000)]
    });
  }

  novoEvento(template: any): void {
    this.categoria = null;
    this.openModal(template, 'post');
  }

  salvarAlteracao(template: any): void{
    if (this.registerForm.valid){

        this.categoria = Object.assign({arquivos: [], avaliacoes: [] }, this.registerForm.value);
        this.categoriaService.postCategoria(this.categoria).subscribe(
          (novaCategoria: Categoria) => {
            template.hide();
            this.getCategorias();
            document.getElementById('refreshNavBar').click();
            this.toastr.success('Cadastrado com Sucesso!');
          }, error => {
            this.toastr.error(`Erro ao inserir: ${error}`);
          }
        );
    }
  }

}
