import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { InvokedMethod } from '../../models/InvokedMethod';
import { LocalVariable } from '../../models/localvariable';

@Component({
  selector: 'app-setinvokedmethodtolocalvariable',
  imports: [CommonModule, FormsModule],
  templateUrl: './setinvokedmethodtolocalvariable.component.html',
  styleUrl: './setinvokedmethodtolocalvariable.component.css'
})
export class SetinvokedmethodtolocalvariableComponent implements OnInit {
  @Input() localVariableData!: LocalVariable;
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

  if (!this.localVariableData?.id) {
    this.error = 'Parameter data is missing or invalid.';
    return;
  }

  this.simulatorService.setInvokeMethodToLocalVariable(this.localVariableData.id, this.selectedInvokedMethodId).subscribe({
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
