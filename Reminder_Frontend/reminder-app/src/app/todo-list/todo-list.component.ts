import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AddTodoDialogComponent } from '../add-todo-dialog/add-todo-dialog.component';
import { Todo } from '../models/Todo';
import { DialogService } from '../services/dialog.service';
import { ErrorService } from '../services/error.service';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css'],
})
export class TodoListComponent implements OnInit {
  todos: Todo[] = [];
  dialogRef!: MatDialogRef<AddTodoDialogComponent>;

  constructor(
    private todoService: TodoService,
    private dialogService: DialogService,
    private errorService: ErrorService
  ) {}

  ngOnInit(): void {
    this.getTodos();
  }

  getTodos = () => {
    this.todoService.getTodos().subscribe((res) => {
      this.todos = res;
    });
  };

  openAddTodoDialog() {
    this.dialogRef = this.dialogService.openDialog(
      '350px',
      {
        onSave: this.addTodo,
        buttonText: 'Add',
        paragraphText: 'What todo do you want to add?',
      },
      AddTodoDialogComponent
    );
  }

  addTodo = (todo: Todo) => {
    this.todoService.createTodo(todo).subscribe({
      error: (e) => {
        if (e.error == 'Invalid date') {
          this.errorService.showError('Todo date cannot be earlier than now.');
        } else this.errorService.showError('Invalid todo creation attempt.');
      },
      next: (res) => {
        this.todos = [...this.todos, res];
        this.dialogService.closeDialog(this.dialogRef);
        console.log('Todo created');
      },
    });
  };
}
