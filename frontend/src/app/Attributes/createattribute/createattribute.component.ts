import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Type } from '../../models/Type';
import { KeyWord } from '../../models/KeyWord';
import { Accessibility } from '../../models/Accessibility';
import { Attribute } from '../../models/attribute';
import { SimulatorService } from '../../services/simulator.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-createattribute',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './createattribute.component.html',
  styleUrls: ['./createattribute.component.css']
})
export class CreateattributeComponent implements OnInit {
  attributeForm!: FormGroup;
  types: Type[] = [];
  keyWords = Object.values(KeyWord).filter(v => isNaN(Number(v)));
  accessibilities = Object.values(Accessibility).filter(v => isNaN(Number(v)));
error: string | null = null;
  KeyWord = KeyWord;
  Accessibility = Accessibility;

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {}

  ngOnInit(): void {
    this.attributeForm = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      instanceType: ['', Validators.required],
      accessibility: [null, Validators.required],
      stereotype: [null],
    });

    this.simulatorService.getTypes().subscribe({
      next: (types) => this.types = types,
      error: (err) => console.error('Error loading types', err)
    });
  }

onSubmit(): void {
  if (this.attributeForm.invalid) return;

  const formValue = this.attributeForm.value;
  const attribute: Attribute = {
    id: '',
    name: formValue.name,
    accessibility: Number(formValue.accessibility),
    instanceType: this.types.find(t => t.name === formValue.instanceType)!,
    referenceType: this.types.find(t => t.name === formValue.type)!,
    stereotype: formValue.stereotype !== '' && formValue.stereotype !== null
      ? Number(formValue.stereotype)
      : null,
  };

  this.error = null;

  this.simulatorService.createAttribute(attribute).subscribe({
    next: (res) => alert(`Attribute created with ID: ${res.id}`),
    error: (err) => {
      this.error = err?.error?.message || err.message || 'Error creating attribute';
    }
  });
}
}
