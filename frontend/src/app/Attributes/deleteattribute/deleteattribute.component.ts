import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulatorService } from '../../services/simulator.service';
import { Attribute } from '../../models/attribute';


@Component({
  selector: 'app-deleteattribute',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './deleteattribute.component.html',
  styleUrl: './deleteattribute.component.css'
})
export class DeleteattributeComponent {
    @Input() attributeData!: Attribute;
  @Output() close = new EventEmitter<void>();
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  onDelete(): void {
    if (!this.attributeData?.id) {
      this.error = 'Invalid attribute selected.';
      return;
    }

    this.simulatorService.deleteAttribute(this.attributeData.id).subscribe({
      next: () => {
        this.message = 'Attribute deleted successfully.';
        this.error = null;
        this.close.emit();
      },
      error: (err) => {
        this.error = 'Error deleting attribute: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
