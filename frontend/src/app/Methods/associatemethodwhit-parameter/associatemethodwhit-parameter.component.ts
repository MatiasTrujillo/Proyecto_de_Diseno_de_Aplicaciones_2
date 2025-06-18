import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Parameter } from '../../models/Parameter';
import { Method } from '../../models/method';

@Component({
  selector: 'app-associatemethodwhit-parameter',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './associatemethodwhit-parameter.component.html',
  styleUrl: './associatemethodwhit-parameter.component.css'
})
export class AssociatemethodwhitParameterComponent implements OnInit {
  @Input() methodData!: Method;
  @Output() close = new EventEmitter<void>();

  parameters: Parameter[] = [];
  selectedParameterId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getParameters().subscribe({
      next: (data) => this.parameters = data,
      error: (err) => this.error = 'Error loading parameters: ' + (err?.message || err)
    });
  }

  associate(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedParameterId) {
      this.error = 'Please select a parameter.';
      return;
    }

    if (!this.methodData?.id) {
      this.error = 'No method selected.';
      return;
    }

    this.simulatorService.addParameterToMethod(this.methodData.id, this.selectedParameterId).subscribe({
      next: (res) => {
        this.message = res.message || 'Parameter associated successfully.';
        this.selectedParameterId = '';
      },
      error: (err) => {
        this.error = err?.error?.message || err?.message || 'Error associating parameter.';
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}