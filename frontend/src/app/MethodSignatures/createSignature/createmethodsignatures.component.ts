import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Type } from '../../models/Type';
import { MethodSignature } from '../../models/MethodSignature';
import { Parameter } from '../../models/Parameter'; // Asegúrate de tener este modelo
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-createmethodsignatures',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './createmethodsignatures.component.html',
  styleUrls: ['./createmethodsignatures.component.css']
})
export class CreateMethodSignaturesComponent implements OnInit {
  methodForm!: FormGroup;
  types: Type[] = [];
  parameters: Parameter[] = []; // Todos los parámetros disponibles

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {}

  ngOnInit(): void {
    this.methodForm = this.fb.group({
      name: ['', Validators.required],
      returnType: ['', Validators.required],
      parameters: [[]] // Para selección múltiple
    });

    this.simulatorService.getTypes().subscribe({
      next: (types) => this.types = types,
      error: (err) => console.error('Error loading types', err)
    });

    this.getParameters();
  }

  getParameters(): void {
    this.simulatorService.getParameters().subscribe({
      next: (params) => this.parameters = params,
      error: (err) => console.error('Error loading parameters', err)
    });
  }

  onSubmit(): void {
    if (this.methodForm.invalid) return;

    const formValue = this.methodForm.value;

    const methodSignature: MethodSignature = {
      id: '',
      name: formValue.name,
      returnType: this.types.find(t => t.name === formValue.returnType)!,
      parameters: this.parameters.filter(p => formValue.parameters.includes(p.id))
    };
    this.simulatorService.createMethodSignature(methodSignature).subscribe({
      next: (res) => alert(`MethodSignature created with ID: ${res.id}`),
      error: (err) => console.error('Error creating methodsignature', err)
    });
  }
}