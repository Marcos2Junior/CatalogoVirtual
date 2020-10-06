import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { ProdutoComponent } from './produto/produto.component';
import { LoginComponent } from './user/login/login.component';
import { PerfilComponent } from './user/perfil/perfil.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard]}
    ]
  },
  {
    path: 'produto', component: ProdutoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
