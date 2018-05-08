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


@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule, MatCheckboxModule, MatListModule,
    MatToolbarModule, MatCardModule, MatIconModule
  ],
  providers: [TodoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
