import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulatorService } from '../../services/simulator.service';
import { Class } from '../../models/class';

@Component({
  selector: 'app-deleteclass',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './deleteclass.component.html',
  styleUrls: ['./deleteclass.component.css']
})
export class DeleteclassComponent {
  @Input() classData!: Class;
  @Output() close = new EventEmitter<void>();

  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  onDelete(): void {
    if (!this.classData?.id) {
      this.error = 'Invalid class selected.';
      return;
    }

    this.simulatorService.deleteClass(this.classData.id).subscribe({
      next: () => {
        this.message = 'Class deleted successfully.';
        this.error = null;
        this.close.emit();
      },
      error: (err) => {
        this.error = 'Error deleting class: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
