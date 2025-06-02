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
    name: formValue.name,
    accessibility: Number(formValue.accessibility),
    type: this.types.find(t => t.name === formValue.type)!,
    stereotype: formValue.stereotype !== '' && formValue.stereotype !== null
      ? Number(formValue.stereotype)
      : null,
  };

  console.log('Attribute enviado al backend:', attribute);

  this.simulatorService.createAttribute(attribute).subscribe({
    next: (res) => alert(`Attribute created with ID: ${res.id}`),
    error: (err) => console.error('Error creating attribute', err)
  });
}
}
