import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ptBrLocale, defineLocale } from 'ngx-bootstrap/chronos';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UsuarioService } from 'src/app/_services/usuario.service';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;
  user: User;
  tipoModal: number;
  file: File;

  constructor(
    private authService: AuthService,
    private usuarioService: UsuarioService,
    public router: Router,
    public fb: FormBuilder,
    private toastr: ToastrService,
    private localeService: BsLocaleService,
    private modalService: BsModalService
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit() {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      nome: ['', Validators.required],
      cpf: [],
      apelido: ['', Validators.minLength(4)],
      nascimento: [],
      telefoneFixo: [],
      phoneNumber: [],
      receberEmail: [false],
      receberMensagem: [false],
      concordaTermos: [false],
      imagem: [],
      email: ['', [Validators.required, Validators.email]],
      passwords: this.fb.group(
        {
          password: ['', [Validators.required, Validators.minLength(4)]],
          confirmPassword: ['', Validators.required],
        },
        { validator: this.compararSenhas }
      ),
    });
  }

  compararSenhas(fb: FormGroup) {
    const confirmSenhaCtrl = fb.get('confirmPassword');
    if (
      confirmSenhaCtrl.errors == null ||
      'mismatch' in confirmSenhaCtrl.errors
    ) {
      if (fb.get('password').value !== confirmSenhaCtrl.value) {
        confirmSenhaCtrl.setErrors({ mismatch: true });
      } else {
        confirmSenhaCtrl.setErrors(null);
      }
    }
  }

  uploadImagem(){
    const nomeArquivo = this.user.imagem.split('\\', 3);
    this.user.imagem = nomeArquivo[2];
    this.usuarioService.postUpload(this.file, nomeArquivo[2]).subscribe();
  }

  onFileChange(event){
    if (event.target.files && event.target.files.length){
      this.file = event.target.files;
    }
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
      this.user = Object.assign(
        { password: this.registerForm.get('passwords.password').value },
        this.registerForm.value
      );
      this.uploadImagem();

      this.authService.register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastr.success('Cadastro Realizado');
        },
        (error) => {
          const erro = error.error;
          erro.forEach((element) => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Cadastro Duplicado!');
                break;
              default:
                this.toastr.error(`Erro no Cadatro! CODE: ${element.code}`);
                break;
            }
          });
        }
      );
    }
  }

  openModal(template: any, tipo: number) {
    this.tipoModal = tipo;
    template.show();
  }
}
