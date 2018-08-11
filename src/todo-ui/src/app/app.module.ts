import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule,
        MatCheckboxModule,
        MatListModule,
        MatToolbar,
        MatToolbarModule,
        MatCardModule,
        MatIconModule} from '@angular/material';

import { AppComponent } from './app.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoService } from './todo.service';

import { OAuthModule } from 'angular-oauth2-oidc';
import { AuthGuard } from './auth.gaurd';
import { RouterModule } from '@angular/router';
import { TokenInterceptor } from './token.interceptor';
import { CurrentUserComponent } from './current-user/current-user.component';


@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    CurrentUserComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    BrowserAnimationsModule,
    MatButtonModule, MatCheckboxModule, MatListModule,
    MatToolbarModule, MatCardModule, MatIconModule,
    RouterModule.forRoot([
      { path: 'home', component: TodoListComponent, canActivate: [AuthGuard] },
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    TodoService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
