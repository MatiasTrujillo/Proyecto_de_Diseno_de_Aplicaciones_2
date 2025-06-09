import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { UpdateAttribute } from '../../models/UpdateAttribute';
import { Attribute } from '../../models/attribute';
import { Type } from '../../models/Type';
import { KeyWord } from '../../models/KeyWord';
import { Accessibility } from '../../models/Accessibility';

@Component({
  selector: 'app-updateattribute',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './updateattribute.component.html',
  styleUrls: ['./updateattribute.component.css']
})
export class UpdateattributeComponent implements OnInit {
  updateForm!: FormGroup;

  @Input() attributeData!: Attribute;
  @Output() close = new EventEmitter<void>();

  attributes: Attribute[] = [];
  attributeId: string = '';
  types: Type[] = [];
  message = '';
  error = '';

  AccessibilityWordLabels = Object.keys(Accessibility);
  accessibilitys = Object.keys(Accessibility).filter(k => isNaN(Number(k)));

  keyWordLabels = Object.keys(KeyWord);
  keyWords = Object.keys(KeyWord).filter(k => isNaN(Number(k)));

  constructor(private fb: FormBuilder, private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.loadAttributes();

    if (this.attributeData) {
      this.attributeId = this.attributeData.id;

      this.updateForm = this.fb.group({
        name: [this.attributeData.name, Validators.required],
        IdReferenceType: [this.attributeData.ReferenceType, Validators.required],
        IdInstanceType: [this.attributeData.InstanceType, Validators.required],
        stereotype: [this.attributeData.stereotype ?? null],
        accessibility: [this.attributeData.accessibility ?? null]
      });
    }

    this.simulatorService.getTypes().subscribe({
      next: types => this.types = types,
      error: err => this.error = 'Error loading types.'
    });
  }

  loadAttributes(): void {
    this.simulatorService.getAttribute().subscribe({
      next: (data) => this.attributes = data,
      error: () => this.error = 'Error loading attributes.'
    });
  }

  getKeyValue(label: string): number {
    return KeyWord[label as keyof typeof KeyWord];
  }

  getAcceValue(label: string): number {
    return Accessibility[label as keyof typeof Accessibility];
  }

  onSubmit(): void {
     console.log('onSubmit called', this.updateForm.value); // <-- Agrega esto
    this.message = '';
    this.error = '';

    const updateData: Partial<UpdateAttribute> = this.updateForm.value;

    this.simulatorService.updateAttribute(this.attributeId, updateData).subscribe({
      next: res => {
        this.message = res?.message ?? 'Attribute updated successfully.';
      },
      error: err => {
        this.error = err?.error?.message ?? 'Update failed.';
      }
    });
  }

  closeForm(): void {
    this.close.emit();
  }
}
