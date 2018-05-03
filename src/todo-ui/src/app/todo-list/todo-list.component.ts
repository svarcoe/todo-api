import { Component, OnInit } from '@angular/core';
import { TodoService } from '../todo.service';
import { Todo } from '../todo';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  todos: Todo[];
  getTodos() {
    this.todoService.getTodos().subscribe(todos => this.todos = todos);
  }
  constructor(
    private todoService: TodoService
  ) { }

  ngOnInit() {
    this.getTodos();
  }

}
