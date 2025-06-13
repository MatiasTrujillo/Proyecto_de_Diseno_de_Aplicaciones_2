import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { InvokedMethod } from '../../models/InvokedMethod';
import { Parameter } from '../../models/Parameter';

@Component({
  selector: 'app-set-invoke-method-to-parameter',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './set-invoke-method-to-parameter.component.html',
  styleUrl: './set-invoke-method-to-parameter.component.css'
})
export class SetInvokeMethodToParameterComponent implements OnInit {
  @Input() parameterData!: Parameter;
  @Output() close = new EventEmitter<void>();

  invokedMethods: InvokedMethod[] = [];
  selectedInvokedMethodId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getInvokedMethods().subscribe({
      next: (data) => this.invokedMethods = data,
      error: (err) => this.error = 'Error loading invoked methods: ' + String(err?.message || err || 'Unknown error')
    });
  }

 associate(): void {
  this.message = null;
  this.error = null;

  if (!this.selectedInvokedMethodId) {
    this.error = 'Please select an invoked method.';
    return;
  }

  if (!this.parameterData?.id) {
    this.error = 'Parameter data is missing or invalid.';
    return;
  }

  this.simulatorService.setInvokeMethodToParameter(this.parameterData.id, this.selectedInvokedMethodId).subscribe({
    next: (res) => {
      this.message = res.message || 'Successfully associated invoked method to parameter.';
      this.selectedInvokedMethodId = '';
    },
    error: (err) => {
      this.error = 'Error associating invoked method: ' + String(err?.message || err || 'Unknown error');
    }
  });
}

  closeModal(): void {
    this.close.emit();
  }
}
