import { Component } from '@angular/core';
import { Interface } from '../../models/Interface';
import { SimulatorService } from '../../services/simulator.service';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule  } from '@angular/material/snack-bar';

@Component({
  selector: 'app-createinterface',
  imports: [FormsModule, CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatSnackBarModule],
  templateUrl: './createinterface.component.html',
  styleUrl: './createinterface.component.css'
})
export class CreateinterfaceComponent {
  interfaceName: string = '';
  message: string = '';
  isError: boolean = false;

  constructor(private simulatorService: SimulatorService, private snackBar: MatSnackBar) {}

 createInterface() {
  if (!this.interfaceName) {
    this.snackBar.open('Por favor, completá todos los campos.', 'Cerrar', {
      duration: 3000,
      panelClass: ['snack-error']
    });
    return;
  }

  const interfaceDto: Interface = {
    name: this.interfaceName,
  };

  this.simulatorService.createInterface(interfaceDto).subscribe({
    next: (res) => {
      this.snackBar.open(`Interfaz creada con éxito. ID: ${res.id}`, 'Cerrar', {
        duration: 3000,
        panelClass: ['snack-success']
      });
      this.interfaceName = '';
    },
    error: (err) => {
      const errorMessage =
        err?.error?.message || 'Ocurrió un error al crear la interfaz.';
      this.snackBar.open(errorMessage, 'Cerrar', {
        duration: 4000,
        panelClass: ['snack-error']
      });
    }
  });
 }
}
