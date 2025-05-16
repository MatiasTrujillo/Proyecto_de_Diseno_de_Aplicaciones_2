import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // 👈 Importar esto
import { SimulatorService } from '../services/simulator.service';
import { Class } from '../models/class';
import { Stereotype } from '../models/stereotype';

@Component({
  selector: 'app-create-class',
  standalone: true,
  imports: [FormsModule, CommonModule], // 👈 AÑADIR CommonModule
  templateUrl: './create-class.component.html',
  styleUrls: ['./create-class.component.css']
})
export class CreateClassComponent {
  Stereotype = Stereotype;

  newClass: Class = {
    name: '',
    Stereotype: Stereotype.Public
  };

  constructor(private simulatorService: SimulatorService, private router: Router) {}

  onSubmit() {
    this.simulatorService.createClass(this.newClass).subscribe({
      next: () => {
        alert('Class created successfully!');
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Error creating class:', err);
        alert('Error creating class.');
      }
    });
  }

  getStereotypeEntries(): [string, string][] {
    return Object.entries(Stereotype);
  }
}
