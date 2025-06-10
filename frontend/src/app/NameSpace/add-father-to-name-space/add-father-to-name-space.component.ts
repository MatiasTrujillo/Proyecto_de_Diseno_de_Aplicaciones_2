import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { NameSpace } from '../../models/NameSpace';
import { NameSpaceComponent } from '../../models/NameSpaceComponent';
import { SimulatorService } from '../../services/simulator.service';
import { FormsModule } from '@angular/forms';
import { NgIf, NgForOf } from '@angular/common';


@Component({
  selector: 'app-add-father-to-name-space',
  standalone: true,
  imports: [FormsModule, NgIf, NgForOf],
  templateUrl: './add-father-to-name-space.component.html',
  styleUrls: ['./add-father-to-name-space.component.css']
})
export class AddFatherToNameSpaceComponent implements OnInit {
  @Input() nameSpaceData!: NameSpace;
  @Output() close = new EventEmitter<void>();

  nameSpaceComponents: NameSpaceComponent[] = [];
  selectedFatherId: string | null = null;

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
        c => c.id !== this.nameSpaceData.id
      );
    },
    error: (err) => {
      if (err.error && err.error.message) {
        this.error = err.error.message;
      } else {
        this.error = 'Error loading namespace components.';
      }
    }
  });
}

associateFather(): void {
  if (!this.selectedFatherId) return;

  this.simulatorService.addFatherToNameSpace(this.selectedFatherId, this.nameSpaceData.id).subscribe({
    next: () => {
      this.message = 'Father associated successfully.';
      this.error = null;
    },
    error: (err) => {
      this.message = null;

      // Intenta acceder al mensaje del backend si existe
      if (err.error && err.error.message) {
        this.error = err.error.message;
      } else {
        this.error = 'Failed to associate father.';
      }
    }
  });
}

  closeModal(): void {
    this.close.emit();
  }
}
