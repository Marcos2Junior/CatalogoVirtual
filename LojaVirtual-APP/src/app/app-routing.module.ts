import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { AuthGuard } from './auth/auth.guard';
import { CategoriaComponent } from './admin/categoria/categoria.component';
import { ProdutoComponent } from './admin/produto/produto.component';
import { LoginComponent } from './user/login/login.component';
import { PerfilComponent } from './user/perfil/perfil.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { DestaqueComponent } from './admin/destaque/destaque.component';

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
    path: 'admin', component: AdminComponent, canActivate: [AuthGuard],
    children: [
      { path: 'categoria', component: CategoriaComponent },
      { path: 'produto', component: ProdutoComponent },
      { path: 'destaque', component: DestaqueComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
