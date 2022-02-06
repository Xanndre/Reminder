import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../models/Todo';
import { TodoCreate } from '../models/TodoCreate';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  constructor(private client: HttpClient) {}

  getTodos(): Observable<Todo[]> {
    return this.client.get<Todo[]>('https://localhost:44340/api/Todos');
  }

  deleteTodo(id: number) {
    return this.client.delete(`https://localhost:44340/api/Todos/${id}`);
  }
  
  createTodo(todo: TodoCreate){
    return this.client
      .post<Todo>('https://localhost:44340/api/Todos', todo);
  }

  updateTodo(todo: Todo){
    return this.client
      .put<Todo>(`https://localhost:44340/api/Todos/${todo.id}`, todo);
  }
}
