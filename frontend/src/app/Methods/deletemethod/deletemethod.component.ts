import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulatorService } from '../../services/simulator.service';
import { Method } from '../../models/method';


@Component({
  selector: 'app-deletemethod',
   standalone: true,
  imports: [CommonModule],
  templateUrl: './deletemethod.component.html',
  styleUrls: ['./deletemethod.component.css']
})
export class DeletemethodComponent {
   @Input() methodData!: Method;
   @Output() close = new EventEmitter<void>();

  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  onDelete(): void {
    if (!this.methodData?.id) {
      this.error = 'Invalid method selected.';
      return;
    }

    this.simulatorService.deleteMethod(this.methodData.id).subscribe({
      next: () => {
        this.message = 'Method deleted successfully.';
        this.error = null;
        this.close.emit();
      },
      error: (err) => {
        this.error = 'Error deleting method: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
