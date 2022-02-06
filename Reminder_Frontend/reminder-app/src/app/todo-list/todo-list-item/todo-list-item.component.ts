import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Todo } from 'src/app/models/Todo';
import { AddNotificationDialogComponent } from 'src/app/add-notification-dialog/add-notification-dialog.component';
import { TodoService } from 'src/app/services/todo.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notification } from 'src/app/models/Notification';
import { AddTodoDialogComponent } from 'src/app/add-todo-dialog/add-todo-dialog.component';
import { DialogService } from 'src/app/services/dialog.service';
import { ErrorService } from 'src/app/services/error.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-todo-list-item',
  templateUrl: './todo-list-item.component.html',
  styleUrls: ['./todo-list-item.component.css'],
})
export class TodoListItemComponent implements OnInit {
  @Input() todo!: Todo;
  @Output() reloadTodosEvent = new EventEmitter<void>();

  editTodoDialogRef!: MatDialogRef<AddTodoDialogComponent>;
  notificationDialogRef!: MatDialogRef<AddNotificationDialogComponent>;

  reloadTodos() {
    this.reloadTodosEvent.next();
  }

  constructor(
    private todoService: TodoService,
    private notificationService: NotificationService,
    private dialogService: DialogService,
    private errorService: ErrorService
  ) {}

  ngOnInit() {}

  openAddNotificationDialog() {
    this.notificationDialogRef = this.dialogService.openDialog(
      '350px',
      {
        todo: this.todo,
        onSave: this.addNotification,
        buttonText: 'Add',
        paragraphText: 'What notification do you want to add?',
      },
      AddNotificationDialogComponent
    );
  }

  openEditNotificationDialog(notification: Notification) {
    this.notificationDialogRef = this.dialogService.openDialog(
      '350px',
      {
        todo: this.todo,
        notification: notification,
        onSave: this.editNotification,
        buttonText: 'Edit',
        paragraphText: "Let's edit this notification!",
      },
      AddNotificationDialogComponent
    );
  }

  deleteTodo() {
    this.todoService.deleteTodo(this.todo.id).subscribe(() => {
      this.reloadTodos();
      console.log('Todo deleted');
    });
  }

  deleteNotification(id: number) {
    this.notificationService.deleteNotification(id).subscribe(() => {
      this.reloadTodos();
      console.log('Notification deleted');
    });
  }

  openEditTodoDialog(todo: Todo) {
    this.editTodoDialogRef = this.dialogService.openDialog(
      '350px',
      {
        todo: todo,
        onSave: this.editTodo,
        buttonText: 'Edit',
        paragraphText: "Let's edit this todo!",
        warningText:
          'Attention! If you edit a task, all notifications for that task will be deleted.',
      },
      AddTodoDialogComponent
    );
  }

  editTodo = (todo: Todo) => {
    this.todoService.updateTodo(todo).subscribe({
      error: (e) => {
        if (e.error == 'Invalid date') {
          this.errorService.showError('Todo date cannot be earlier than now.');
        } else this.errorService.showError('Invalid todo creation attempt.');
      },
      next: (res) => {
        this.todo.title = res.title;
        this.todo.description = res.description;
        this.todo.date = res.date;
        this.dialogService.closeDialog(this.editTodoDialogRef);
        console.log('Todo updated');
      },
    });
  };

  addNotification = (notification: Notification) => {
    this.notificationService.createNotification(notification).subscribe({
      error: (e) => {
        if (e.error == 'Invalid date') {
          this.errorService.showError(
            'Notification date cannot be earlier than now and later than date of an event.'
          );
        } else
          this.errorService.showError('Invalid notification creation attempt.');
      },
      next: (res) => {
        this.todo.notifications = [...this.todo.notifications, res];
        this.dialogService.closeDialog(this.notificationDialogRef);
        console.log('Notification created');
      },
    });
  };

  editNotification = (notification: Notification) => {
    this.notificationService.updateNotification(notification).subscribe({
      error: (e) => {
        if (e.error == 'Invalid date') {
          this.errorService.showError(
            'Notification date cannot be earlier than now and later than date of an event.'
          );
        } else
          this.errorService.showError('Invalid notification creation attempt.');
      },
      next: () => {
        this.reloadTodos();
        this.dialogService.closeDialog(this.notificationDialogRef);
        console.log('Notification updated');
      },
    });
  };
}
