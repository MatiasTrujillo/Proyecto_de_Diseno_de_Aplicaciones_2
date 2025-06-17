import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { SimulatorService } from '../../services/simulator.service';
import { Attribute } from '../../models/attribute';
import { Class } from '../../models/class';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-associateattributewithclass',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './associateattributewithclass.component.html',
  styleUrl: './associateattributewithclass.component.css'
})
export class AssociateattributewithclassComponent implements OnInit {
  @Input() attributeData!: Attribute;
  @Output() close = new EventEmitter<void>();

  classes: Class[] = [];

  selectedAttributeId = '';
  selectedClassId = '';

  message = '';
  error = '';

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    if (this.attributeData) {
      this.selectedAttributeId = this.attributeData.id;
    }
    this.loadClasses();
  }

  loadClasses(): void {
    this.simulatorService.getClasses().subscribe({
      next: (data) => this.classes = data,
      error: () => this.error = 'Error loading classes.'
    });
  }

  associate(): void {
    this.message = '';
    this.error = '';

    if (!this.selectedAttributeId || !this.selectedClassId) {
      this.error = 'Select a class to associate with the attribute.';
      return;
    }

    this.simulatorService.associateAttributeWithClass(this.selectedAttributeId, this.selectedClassId).subscribe({
      next: (res) => this.message = res.message ?? 'Attribute associated successfully.',
      error: (err) => this.error = err?.error?.message ?? 'Association failed.'
    });
  }
}
