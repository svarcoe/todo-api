import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


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
    HttpClientModule
  ],
  providers: [TodoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
