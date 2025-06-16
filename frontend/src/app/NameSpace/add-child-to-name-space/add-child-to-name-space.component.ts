import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { NameSpace } from '../../models/NameSpace';
import { NameSpaceComponent } from '../../models/NameSpaceComponent';
import { SimulatorService } from '../../services/simulator.service';
import { FormsModule } from '@angular/forms';
import { NgIf, NgForOf } from '@angular/common';

@Component({
  selector: 'app-add-child-to-name-space',
  standalone: true,
  imports: [FormsModule, NgIf, NgForOf],
  templateUrl: './add-child-to-name-space.component.html',
  styleUrls: ['./add-child-to-name-space.component.css']
})
export class AddChildToNameSpaceComponent implements OnInit {
  @Input() nameSpaceData!: NameSpace;
  @Output() close = new EventEmitter<void>();

  nameSpaceComponents: NameSpaceComponent[] = [];
  selectedChildId: string | null = null;

  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.loadNameSpaceComponents();
  }

  loadNameSpaceComponents(): void {
    this.simulatorService.getNameSpaceComponents().subscribe({
      next: (components) => {
        this.nameSpaceComponents = components.filter(
          c => c.id !== this.nameSpaceData.id // evitar seleccionarse a sí mismo como hijo
        );
      },
      error: (err) => {
        this.error = err.error?.message || 'Error loading namespace components.';
      }
    });
  }

  associateChild(): void {
    if (!this.selectedChildId) return;

    this.simulatorService.addChildToNameSpace(this.nameSpaceData.id, this.selectedChildId).subscribe({
      next: () => {
        this.message = 'Child associated successfully.';
        this.error = null;
      },
      error: (err) => {
        this.message = null;
        this.error = err.error?.message || 'Failed to associate child.';
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}
