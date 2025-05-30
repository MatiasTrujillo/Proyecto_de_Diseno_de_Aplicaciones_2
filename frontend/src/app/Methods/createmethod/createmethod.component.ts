import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Type } from '../../models/Type';
import { Method } from '../../models/method';
import { KeyWord } from '../../models/KeyWord';
import { Accessibility } from '../../models/Accessibility';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-method',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './createmethod.component.html',
  styleUrls: ['./createmethod.component.css']
})
export class CreateMethodComponent implements OnInit {
  methodForm!: FormGroup;
  types: Type[] = [];
  keyWords = Object.values(KeyWord).filter(v => isNaN(Number(v)));
  accessibilities = Object.values(Accessibility).filter(v => isNaN(Number(v)));

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {}

  ngOnInit(): void {
    this.methodForm = this.fb.group({
      name: ['', Validators.required],
      returnType: ['', Validators.required],
      accessibility: ['', Validators.required],
      stereotype: [''],
      isOverride: [false]
    });

    this.simulatorService.getTypes().subscribe({
      next: (types) => this.types = types,
      error: (err) => console.error('Error loading types', err)
    });
  }

  onSubmit(): void {
    if (this.methodForm.invalid) return;

    const formValue = this.methodForm.value;

    const method: Method = {
      name: formValue.name,
      returnType: this.types.find(t => t.name === formValue.returnType)!,
      accessibility: +formValue.accessibility,
      stereotype: formValue.stereotype !== '' ? +formValue.stereotype : null,
      IsOverride: formValue.isOverride
    };
   console.log('Attribute enviado al backend:', method);
    this.simulatorService.createMethod(method).subscribe({
      next: (res) => alert(`Method created with ID: ${res.id}`),
      error: (err) => console.error('Error creating method', err)
    });
  }
}
