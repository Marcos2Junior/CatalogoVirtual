import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Categoria } from 'src/app/_models/Categoria';
import { CategoriaService } from 'src/app/_services/categoria.service';
import { User } from '../../_models/User';
import { UsuarioService } from '../../_services/usuario.service';
import { KeysApp } from '../../_utils/KeysApp';
import { UrlApi } from '../../_utils/urlApi';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {

  user: User;
  urlImage = UrlApi.UrlImagesUsers + 'indef.png';
  categorias: Categoria[];

  constructor(
    private usuarioService: UsuarioService,
    private categoriaService: CategoriaService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getCategorias();

    if (localStorage.getItem(KeysApp.localStorageJWT) != null) {
      this.SelecionaPerfil();
    }
  }

  getCategorias(): void {
    this.categoriaService.getCategorias().subscribe(
      (categorias: Categoria[]) => {
        this.categorias = categorias;
      },
      (error) => {
        this.toastr.error('Ops! Não foi possível carregar as categorias');
      }
    );
  }

  SelecionaPerfil(): void {
    this.usuarioService.getUserAuth().subscribe(
      (usuario: User) => {
        this.user = usuario;
        this.trataPerfil();
      },
      (error) => {
        this.toastr.error(`Erro ao selecionar Perfil: ${error}`);
      }
    );
  }

  trataPerfil(): void {
    if (this.user && this.user.imagem) {
      this.urlImage = UrlApi.UrlImagesUsers + this.user.imagem;
    } else {
      this.urlImage = UrlApi.UrlImagesUsers + 'indef.png';
    }
  }

  entrar(): void {
    this.router.navigate(['/user/login']);
  }

  logout(): void {
    localStorage.removeItem(KeysApp.localStorageJWT);
    this.toastr.show('Log Out');
    this.router.navigate(['/user/login']);
    this.user = null;
    this.trataPerfil();
  }
}
