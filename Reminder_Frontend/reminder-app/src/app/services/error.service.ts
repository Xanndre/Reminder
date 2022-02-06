import { Injectable } from '@angular/core';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';
import { DialogService } from './dialog.service';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private dialogService: DialogService) {}

  showError(error: string) {
    this.dialogService.openDialog(
      '400px',
      { errorMsg: error },
      ErrorDialogComponent
    );
  }
}
