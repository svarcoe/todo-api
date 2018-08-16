import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule,
        MatCheckboxModule,
        MatListModule,
        MatToolbar,
        MatMenuModule,
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
import { ImportComponent } from './import/import.component';


@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    CurrentUserComponent,
    ImportComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    BrowserAnimationsModule,
    MatMenuModule,
    MatButtonModule, MatCheckboxModule, MatListModule,
    MatToolbarModule, MatCardModule, MatIconModule,
    RouterModule.forRoot([
      { path: 'home', component: TodoListComponent, canActivate: [AuthGuard] },
      { path: 'import', component: ImportComponent, canActivate: [AuthGuard] },
      { path: '**', redirectTo: 'home' }
    ],
    { enableTracing: true })
  ],
  providers: [
    AuthGuard,
    TodoService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
