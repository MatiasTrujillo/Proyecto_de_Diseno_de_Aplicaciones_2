import { Component } from '@angular/core';
import { SimulatorService } from '../services/simulator.service';
import { Class } from '../models/class';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { KeyWord } from '../models/KeyWord';

@Component({
  selector: 'app-create-class',
  standalone: true,
  imports: [FormsModule, CommonModule],
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

  constructor(private simulatorService: SimulatorService) {}


  getKeyValue(label: string): number {
    return KeyWord[label as keyof typeof KeyWord];
  }

  createClass() {
  if (!this.className) {
    this.message = 'Please fill in all fields.';
    this.isError = true;
    return;
  }

  const classDto: Class = {
    name: this.className,
    stereotype: this.stereotype // Puede ser null
  };

  this.simulatorService.createClass(classDto).subscribe({
    next: (res) => {
      this.message = `Class created successfully with ID: ${res.id}`;
      this.isError = false;
      this.className = '';
      this.stereotype = null;
    },
  });
}
}
