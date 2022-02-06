import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DateService } from '../services/date.service';
import { DialogService } from '../services/dialog.service';

@Component({
  selector: 'app-add-notification-dialog',
  templateUrl: './add-notification-dialog.component.html',
  styleUrls: ['./add-notification-dialog.component.css'],
})
export class AddNotificationDialogComponent implements OnInit {
  todayDate: Date = new Date();
  date!: Date;
  time!: string;
  email!: string;
  id!: number;
  buttonText!: string;
  paragraphText!: string;

  constructor(
    private dateService: DateService,
    private dialogService: DialogService,
    private dialogRef: MatDialogRef<AddNotificationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    this.buttonText = this.data.buttonText;
    this.paragraphText = this.data.paragraphText;

    if (this.data.notification) {
      this.email = this.data.notification.email;
      this.date = this.data.notification.date;
      this.time = this.dateService.toTimeString(this.data.notification.date);
      this.id = this.data.notification.id;
    }
  }

  closeDialog() {
    this.dialogService.closeDialog(this.dialogRef);
  }

  addNotification() {
    const notification = {
      id: this.id,
      date: this.dateService.toDateTime(this.time, this.date),
      isCompleted: false,
      email: this.email,
      todoId: this.data.todo.id,
    };

    this.data.onSave(notification);
  }
}
