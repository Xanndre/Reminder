import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  constructor(private dialog: MatDialog) {}

  openDialog<T>(
    width: string,
    data: object,
    dialogComponent: ComponentType<T>
  ) {
    const dialogRef = this.dialog.open(dialogComponent, {
      width: width,
      data: data,
    });

    return dialogRef;
  }

  closeDialog<T>(dialogRef: MatDialogRef<T>) {
    dialogRef.close();
  }
}
