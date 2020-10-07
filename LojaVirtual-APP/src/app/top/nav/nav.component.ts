import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(
    private usuarioService: UsuarioService,
    private toastr: ToastrService,
    public router: Router
  ) {}

  ngOnInit(): void {
    if (localStorage.getItem(KeysApp.localStorageJWT) != null) {
      this.SelecionaPerfil();
    }
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
