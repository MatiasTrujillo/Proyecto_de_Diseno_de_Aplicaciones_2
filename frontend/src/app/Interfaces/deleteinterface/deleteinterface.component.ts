import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimulatorService } from '../../services/simulator.service';
import { Interface } from '../../models/Interface';

@Component({
  selector: 'app-deleteinterface',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './deleteinterface.component.html',
  styleUrls: ['./deleteinterface.component.css']
})
export class DeleteinterfaceComponent implements OnInit {
  @Input() interfaceData!: Interface;
  @Output() close = new EventEmitter<void>();

  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {}

  onDelete(): void {
    if (!this.interfaceData?.id) {
      this.error = 'No interface selected to delete.';
      return;
    }

    this.simulatorService.deleteInterface(this.interfaceData.id).subscribe({
      next: () => {
        this.message = 'Interface deleted successfully.';
        this.error = null;
        setTimeout(() => this.close.emit(), 1200);
      },
      error: (err) => {
        this.error = 'Error deleting interface: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}