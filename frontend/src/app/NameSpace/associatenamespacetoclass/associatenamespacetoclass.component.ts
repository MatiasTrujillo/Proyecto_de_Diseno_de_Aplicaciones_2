import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NameSpace } from '../../models/NameSpace';
import { Class } from '../../models/class';
import { SimulatorService } from '../../services/simulator.service';

@Component({
  selector: 'app-associatenamespacetoclass',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './associatenamespacetoclass.component.html',
  styleUrl: './associatenamespacetoclass.component.css'
})
export class AssociatenamespacetoclassComponent implements OnInit {

  @Input() nameSpaceData!: NameSpace;
  @Output() close = new EventEmitter<void>();

  classes: Class[] = [];
  selectedClassId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getClasses().subscribe({
      next: (data) => this.classes = data,
      error: (err) => this.error = 'Error loading classes: ' + (err?.message || err)
    });
  }

  associate(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedClassId) {
      this.error = 'Please select a class.';
      return;
    }

    this.simulatorService.associateNameSpaceToClass(this.nameSpaceData.id, this.selectedClassId).subscribe({
      next: () => {
        this.message = 'Successfully associated class to namespace.';
        this.selectedClassId = '';
      },
      error: (err) => {
        this.error = 'Error associating class: ' + (err?.message || err);
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}
