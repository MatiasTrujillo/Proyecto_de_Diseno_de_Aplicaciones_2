import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Parameter } from '../../models/Parameter';
import { Type } from '../../models/Type';

@Component({
  selector: 'app-createparameter',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './createparameter.component.html',
  styleUrls: ['./createparameter.component.css']
})
export class CreateparameterComponent implements OnInit {
  name: string = '';
  selectedReferenceTypeId: string | null = null;
  selectedInstanceTypeId: string | null = null;

  types: Type[] = [];
  message: string = '';
  error: string = '';

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getTypes().subscribe({
      next: (types) => this.types = types,
      error: () => this.error = 'Error loading types'
    });
  }

  createParameter(): void {
    this.message = '';
    this.error = '';

    if (!this.name || !this.selectedReferenceTypeId || !this.selectedInstanceTypeId) {
      this.error = 'Name, Reference Type and Instance Type are required.';
      return;
    }

    const referenceType = this.types.find(t => t.id === this.selectedReferenceTypeId);
    const instanceType = this.types.find(t => t.id === this.selectedInstanceTypeId);

    if (!referenceType || !instanceType) {
      this.error = 'Selected types not found.';
      return;
    }

    const parameter: Parameter = {
      name: this.name,
      ReferenceType: referenceType,
      InstanceType: instanceType
    };

    this.simulatorService.createParameter(parameter).subscribe({
      next: (res) => {
        this.message = 'Parameter created! ID: ' + res.id;
        this.name = '';
        this.selectedReferenceTypeId = null;
        this.selectedInstanceTypeId = null;
      },
      error: (err) => {
        this.error = err?.error?.message || 'Error creating parameter';
      }
    });
  }
}
