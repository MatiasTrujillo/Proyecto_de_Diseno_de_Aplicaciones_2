import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { MethodSignature } from '../../models/MethodSignature';
import { Interface } from '../../models/Interface';

@Component({
  selector: 'app-associate-interface-signature',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './associate-interface-signature.component.html',
  styleUrl: './associate-interface-signature.component.css'
})
export class AssociateInterfaceSignatureComponent implements OnInit {
  @Input() interfaceData!: Interface;
  @Output() close = new EventEmitter<void>();

  methodSignatures: MethodSignature[] = [];
  selectedMethodSignatureId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getMethodSignatures().subscribe({
      next: (data) => this.methodSignatures = data,
      error: (err) => this.error = 'Error loading method signatures: ' + (err?.message || err)
    });
  }

  associate(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedMethodSignatureId) {
      this.error = 'Please select a method signature.';
      return;
    }

    if (!this.interfaceData?.id) {
      this.error = 'No interface selected.';
      return;
    }

    this.simulatorService.associateInterfaceSignature(this.interfaceData.id, this.selectedMethodSignatureId).subscribe({
  next: (res) => {
    this.message = res.message || 'Successfully associated method signature to interface.';
    this.selectedMethodSignatureId = '';
  },
  error: (err) => {
    this.error = err?.error?.message || err?.error?.detail || err?.message || 'Error associating method signature.';
  }
});
  }

  closeModal(): void {
    this.close.emit();
  }
}