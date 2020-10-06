import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/User';
import { UsuarioService } from '../_services/usuario.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  user: User;
  baseURL = 'http://localhost:53589/resources/images/';
  urlImage = `${this.baseURL}indef.png`;

  constructor(private usuarioService: UsuarioService,
              private toastr: ToastrService,
              public router: Router) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null) {
      this.SelecionaPerfil();
    }

  }

  SelecionaPerfil(): void{
    this.usuarioService.getUserAuth().subscribe(
      (usuario: User) => {
        this.user = usuario;
        this.trataPerfil();
      }, error => {
        this.toastr.error(`Erro ao selecionar Perfil: ${error}`);
      });
  }

  trataPerfil(): void{
    if (this.user && this.user.imagem){
      this.urlImage = `${this.baseURL}${this.user.imagem}`;
    }
    else{
      this.urlImage = `${this.baseURL}indef.png`;
    }
  }

  entrar(): void {
    this.router.navigate(['/user/login']);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.toastr.show('Log Out');
    this.router.navigate(['/user/login']);
    this.user = null;
    this.trataPerfil();
  }
}
