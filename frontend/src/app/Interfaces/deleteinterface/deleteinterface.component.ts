import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Interface } from '../../models/Interface';

@Component({
  selector: 'app-deleteinterface',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './deleteinterface.component.html',
  styleUrls: ['./deleteinterface.component.css']
})
export class DeleteinterfaceComponent implements OnInit {
  deleteForm!: FormGroup;
  interfaces: Interface[] = [];
  message: string | null = null;
  error: string | null = null;

  constructor(
    private simulatorService: SimulatorService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.deleteForm = this.fb.group({
      id: ['']
    });

    this.loadInterfaces();
  }

  loadInterfaces(): void {
    this.simulatorService.getInterfaces().subscribe({
      next: (res) => this.interfaces = res,
      error: (err) => this.error = 'Error loading interfaces'
    });
  }

  onDelete(): void {
    const id = this.deleteForm.value.id;

    if (!id) {
      this.error = 'Please select an interface to delete.';
      return;
    }

    this.simulatorService.deleteInterface(id).subscribe({
      next: () => {
        this.message = 'Interface deleted successfully.';
        this.error = null;
        this.loadInterfaces();
      },
      error: (err) => {
        this.error = 'Error deleting interface: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
