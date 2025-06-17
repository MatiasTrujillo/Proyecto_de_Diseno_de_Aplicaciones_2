import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { InvokedMethod } from '../../models/InvokedMethod';
import { Method } from '../../models/method';

@Component({
  selector: 'app-addmethod',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './addmethod.component.html',
  styleUrl: './addmethod.component.css'
})
export class AddmethodComponent implements OnInit {
  @Input() methodData!: Method;
  @Output() close = new EventEmitter<void>();

  invokedMethods: InvokedMethod[] = [];
  selectedInvokedMethodId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getInvokedMethods().subscribe({
      next: (data) => this.invokedMethods = data,
      error: (err) => this.error = 'Error loading invoked methods: ' + (err?.message || err)
    });
  }

  addMethod(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedInvokedMethodId) {
      this.error = 'Please select an invoked method.';
      return;
    }

    if (!this.methodData?.id) {
      this.error = 'No method selected.';
      return;
    }

    this.simulatorService.addAMethod(this.methodData.id, this.selectedInvokedMethodId).subscribe({
      next: (res) => {
        this.message = res.message || 'Method associated successfully.';
        this.selectedInvokedMethodId = '';
      },
      error: (err) => {
        this.error = err?.error?.message || err?.message || 'Error associating method.';
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}