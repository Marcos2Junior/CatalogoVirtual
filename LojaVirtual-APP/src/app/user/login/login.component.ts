import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { KeysApp } from 'src/app/_utils/KeysApp';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(
    private authService: AuthService,
    public router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    if (localStorage.getItem(KeysApp.localStorageJWT) != null) {
      this.router.navigate(['/user/perfil']);
    }
  }

  login(): void {
    this.authService.login(this.model).subscribe(
      () => {
        this.router.navigate(['/user/perfil']);
        this.toastr.success('Logado com sucesso!');
        document.getElementById('refreshPerfil').click();
      },
      (error) => {
        this.toastr.error('Falha ao tentar Logar');
      }
    );
  }
}
