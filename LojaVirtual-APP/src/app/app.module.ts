import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AppRoutingModule } from './app-routing.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { NgxMaskModule, IConfig } from 'ngx-mask';

import { AuthInterceptor } from './auth/auth.interceptor';

import { AppComponent } from './app.component';
import { ProdutoComponent } from './admin/produto/produto.component';
import { NavComponent } from './top/nav/nav.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { PerfilComponent } from './user/perfil/perfil.component';
import { TopComponent } from './top/top.component';
import { CarouselComponent } from './top/carousel/carousel.component';
import { CategoriaComponent } from './admin/categoria/categoria.component';
import { AdminComponent } from './admin/admin.component';
import { DestaqueComponent } from './admin/destaque/destaque.component';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

const maskConfig: Partial<IConfig> = {
  validation: true
};

@NgModule({
  declarations: [
    AppComponent,
    ProdutoComponent,
    NavComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    PerfilComponent,
    TopComponent,
    CarouselComponent,
    CategoriaComponent,
    AdminComponent,
      DestaqueComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    CommonModule,
    CurrencyMaskModule,
    NgxMaskModule.forRoot(maskConfig)
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
