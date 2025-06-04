import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Attribute } from '../../models/attribute';


@Component({
  selector: 'app-deleteattribute',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './deleteattribute.component.html',
  styleUrl: './deleteattribute.component.css'
})
export class DeleteattributeComponent {
deleteForm!: FormGroup;
  attributes: Attribute[] = [];
  message: string | null = null;
  error: string | null = null;

  constructor(
    private simulatorService: SimulatorService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.deleteForm = this.fb.group({
      attributeId: ['']
    });

    this.loadAttributes();
  }

  loadAttributes(): void {
    this.simulatorService.getAttribute().subscribe({
      next: (res) => this.attributes = res,
      error: (err) => this.error = 'Error loading classes'
    });
  }

  onDelete(): void {
    const attributeId = this.deleteForm.value.attributeId;

    if (!attributeId) {
      this.error = 'Please select a attribute to delete.';
      return;
    }

    this.simulatorService.deleteAttribute(attributeId).subscribe({
      next: () => {
        this.message = 'Attribute deleted successfully.';
        this.error = null;
        this.loadAttributes();
      },
      error: (err) => {
        this.error = 'Error deleting attribute: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
