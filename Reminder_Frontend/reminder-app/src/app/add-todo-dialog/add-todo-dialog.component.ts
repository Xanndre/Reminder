import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DateService } from '../services/date.service';
import { DialogService } from '../services/dialog.service';

@Component({
  selector: 'app-add-todo-dialog',
  templateUrl: './add-todo-dialog.component.html',
  styleUrls: ['./add-todo-dialog.component.css'],
})
export class AddTodoDialogComponent implements OnInit {
  todayDate: Date = new Date();
  date!: Date;
  time!: string;
  title!: string;
  description!: string;
  id!: number;
  buttonText!: string;
  paragraphText!: string;
  warningText!: string;

  constructor(
    private dateService: DateService,
    private dialogService: DialogService,
    private dialogRef: MatDialogRef<AddTodoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    this.buttonText = this.data.buttonText;
    this.paragraphText = this.data.paragraphText;
    this.warningText = this.data.warningText;

    if (this.data.todo) {
      this.id = this.data.todo.id;
      this.title = this.data.todo.title;
      this.description = this.data.todo.description;
      this.date = this.data.todo.date;
      this.time = this.dateService.toTimeString(this.data.todo.date);
    }
  }

  closeDialog() {
    this.dialogService.closeDialog(this.dialogRef);
  }

  addTodo() {
    const todo = {
      id: this.id,
      title: this.title,
      description: this.description,
      date: this.dateService.toDateTime(this.time, this.date),
      notifications: [],
    };

    this.data.onSave(todo);
  }
}
