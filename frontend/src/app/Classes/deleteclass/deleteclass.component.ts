import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Input, Output, EventEmitter } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Class } from '../../models/class';

@Component({
  selector: 'app-deleteclass',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './deleteclass.component.html',
  styleUrls: ['./deleteclass.component.css']
})
export class DeleteclassComponent implements OnInit {
  @Input() classData: any;
  @Output() close = new EventEmitter<void>();
  deleteForm!: FormGroup;
  classes: Class[] = [];
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

    this.loadClasses();
  }

  loadClasses(): void {
    this.simulatorService.getClasses().subscribe({
      next: (res) => this.classes = res,
      error: (err) => this.error = 'Error loading classes'
    });
  }

  onDelete(): void {
    const id = this.deleteForm.value.id;

    if (!id) {
      this.error = 'Please select a class to delete.';
      return;
    }

    this.simulatorService.deleteClass(id).subscribe({
      next: () => {
        this.message = 'Class deleted successfully.';
        this.error = null;
        this.loadClasses(); // refresh list
      },
      error: (err) => {
        this.error = 'Error deleting class: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
