import { Component } from '@angular/core';
import { SimulatorService } from '../services/simulator.service';
import { Class } from '../models/class';
import { KeyWord } from '../models/KeyWord';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule  } from '@angular/material/snack-bar';


@Component({
  selector: 'app-create-class',
  standalone: true,
  imports: [FormsModule, CommonModule,  CommonModule,
  FormsModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatButtonModule,
  MatSnackBarModule
],

  templateUrl: './createclass.component.html',
  styleUrls: ['./createclass.component.css']
})
export class CreateclassComponent {
  className: string = '';
  stereotype: KeyWord | null = null;
  message: string = '';
  isError: boolean = false;


  keyWordLabels = Object.keys(KeyWord);
  keyWords = Object.keys(KeyWord).filter(k => isNaN(Number(k)));

  constructor(private simulatorService: SimulatorService, private snackBar: MatSnackBar) {}


  getKeyValue(label: string): number {
    return KeyWord[label as keyof typeof KeyWord];
  }

 createClass() {
  if (!this.className) {
    this.snackBar.open('Por favor, completá todos los campos.', 'Cerrar', {
      duration: 3000,
      panelClass: ['snack-error']
    });
    return;
  }

  const classDto: Class = {
    name: this.className,
    stereotype: this.stereotype
  };

  this.simulatorService.createClass(classDto).subscribe({
    next: (res) => {
      this.snackBar.open(`Clase creada con éxito. ID: ${res.id}`, 'Cerrar', {
        duration: 3000,
        panelClass: ['snack-success']
      });
      this.className = '';
      this.stereotype = null;
    },
    error: (err) => {
      const errorMessage =
        err?.error?.message || 'Ocurrió un error al crear la clase.';
      this.snackBar.open(errorMessage, 'Cerrar', {
        duration: 4000,
        panelClass: ['snack-error']
      });
    }
  });
}

}

