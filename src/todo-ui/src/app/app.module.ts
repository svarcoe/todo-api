import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
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
import { AuthGuard } from './auth-gaurd';
import { RouterModule } from '@angular/router';
import { CallbackComponent } from './callback.component';


@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    CallbackComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    BrowserAnimationsModule,
    MatButtonModule, MatCheckboxModule, MatListModule,
    MatToolbarModule, MatCardModule, MatIconModule,
    RouterModule.forRoot([
      { path: '', component: CallbackComponent, pathMatch: 'full' },
      { path: 'home', component: TodoListComponent, canActivate: [AuthGuard] },
      { path: '**', redirectTo: '' }
    ]
  ],
  providers: [TodoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
