import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { NameSpaceComponent } from '../../models/NameSpaceComponent';
import { NameSpace } from '../../models/NameSpace';

@Component({
  selector: 'app-removechild',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './removechild.component.html',
  styleUrl: './removechild.component.css'
})
export class RemovechildComponent implements OnInit {
  @Input() nameSpaceData!: NameSpace;
  @Output() close = new EventEmitter<void>();

  nameSpaceComponents: NameSpaceComponent[] = [];
  selectedChildId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getNameSpaceComponents().subscribe({
      next: (data) => this.nameSpaceComponents = data,
      error: (err) => this.error = 'Error loading namespace components: ' + (err?.message || err)
    });
  }

  removeChild(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedChildId) {
      this.error = 'Please select a child to remove.';
      return;
    }

    if (!this.nameSpaceData?.id) {
      this.error = 'No parent namespace selected.';
      return;
    }

    this.simulatorService.removeChildFromNameSpace(this.nameSpaceData.id, this.selectedChildId).subscribe({
      next: () => {
        this.message = 'Child removed successfully.';
        this.selectedChildId = '';
      },
      error: (err) => {
        this.error = err?.error?.message || err?.message || 'Error removing child.';
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}