import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Type } from '../models/Type';
import { LocalVariable } from '../models/localvariable';
import { SimulatorService } from '../services/simulator.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-createlocalvariable',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './createlocalvariable.component.html',
  styleUrl: './createlocalvariable.component.css'
})
export class CreatelocalvariableComponent {
localVariableFrom!: FormGroup;
  localVariableTypes: Type[] = [];

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {}

  ngOnInit(): void {
    this.localVariableFrom = this.fb.group({
      name: ['', Validators.required],
      localVariableType: ['', Validators.required],
    });

    this.simulatorService.getTypes().subscribe({
      next: (localVariableTypes) => this.localVariableTypes = localVariableTypes,
      error: (err) => console.error('Error loading types', err)
    });
  }

  onSubmit(): void {
  if (this.localVariableFrom.invalid) return;

  const formValue = this.localVariableFrom.value;
  const localVariable: LocalVariable = {
    name: formValue.name,
    LocalVariableType: this.localVariableTypes.find(t => t.name === formValue.localVariableType)!
  };

  console.log('Attribute enviado al backend:', localVariable);

  this.simulatorService.createLocalVariable(localVariable).subscribe({
    next: (res) => alert(`Attribute created with ID: ${res.id}`),
    error: (err) => console.error('Error creating attribute', err)
  });
} 
}

