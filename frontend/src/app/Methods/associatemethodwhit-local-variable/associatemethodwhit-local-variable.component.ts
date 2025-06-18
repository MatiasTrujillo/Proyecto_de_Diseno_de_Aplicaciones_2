import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { LocalVariable } from '../../models/localvariable';
import { Method } from '../../models/method';

@Component({
  selector: 'app-associatemethodwhit-local-variable',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './associatemethodwhit-local-variable.component.html',
  styleUrl: './associatemethodwhit-local-variable.component.css'
})
export class AssociatemethodwhitLocalVariableComponent implements OnInit {
  @Input() methodData!: Method;
  @Output() close = new EventEmitter<void>();

  localVariables: LocalVariable[] = [];
  selectedLocalVariableId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getLocalVariables().subscribe({
      next: (data) => this.localVariables = data,
      error: (err) => this.error = 'Error loading local variables: ' + (err?.message || err)
    });
  }

  associate(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedLocalVariableId) {
      this.error = 'Please select a local variable.';
      return;
    }

    if (!this.methodData?.id) {
      this.error = 'No method selected.';
      return;
    }

    this.simulatorService.addLocalVariableToMethod(this.methodData.id, this.selectedLocalVariableId).subscribe({
      next: (res) => {
        this.message = res.message || 'Local variable associated successfully.';
        this.selectedLocalVariableId = '';
      },
      error: (err) => {
        this.error = err?.error?.message || err?.message || 'Error associating local variable.';
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}