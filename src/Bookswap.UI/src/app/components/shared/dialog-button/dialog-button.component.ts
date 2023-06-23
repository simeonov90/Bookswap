import { Component } from '@angular/core';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import { CreateBookFormComponent } from '../../book/create-book-form/create-book-form.component';

@Component({
  selector: 'dialog-button',
  templateUrl: './dialog-button.component.html',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule, CreateBookFormComponent],
})

export class DialogButton {
  constructor(public dialog: MatDialog) {}

  openDialog() {
    this.dialog.open(CreateBookFormComponent);
  }
}
