import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { UsuarioService } from 'src/app/_services/usuario.service';
import { compare } from 'fast-json-patch';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css'],
})
export class PerfilComponent implements OnInit {
  user: User;
  firstName: string;
  apelido: string;

  constructor(
    private usuarioService: UsuarioService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.getUsuario();
  }

  getUsuario(): void {
    this.usuarioService.getUserAuth().subscribe(
      (x) => {
        this.user = x;
        this.firstName = this.user.nome.split(' ', 1)[0];
        this.trataUsuario();
      },
      (error) => {
        this.toastr.error('Não foi possível carregar o seu perfil: ' + error);
      }
    );
  }

  trataUsuario(): void {
    if (this.user.apelido) {
      console.log(this.user.apelido);
    }
  }

  gravaApelido(): void {
    if (!this.apelido){
      // Default é null, define como string vazia para que metodo trataUsuario não exiba o modal de boas vindas
      // é claro, isso se o usuario nao escolher nenhum apelido nesse momento
      this.apelido = '';
    }

    /*
    Foi criado duas dto para usuario, para que use o patch apenas quando forem dados sensiveis que nao e retornado a aplicacao
    como exemplo o password...

    const patch = compare(this.user, { apelido: this.apelido}).filter(x => x.op === 'replace');
    this.usuarioService.patchUsuario(patch, this.user.id).subscribe();*/

    this.user.apelido = this.apelido;
    this.usuarioService.putUsuario(this.user).subscribe();
  }
}
