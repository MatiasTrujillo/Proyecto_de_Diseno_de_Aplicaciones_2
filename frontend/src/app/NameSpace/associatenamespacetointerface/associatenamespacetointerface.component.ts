import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NameSpace } from '../../models/NameSpace';
import { Interface } from '../../models/Interface'; // Asegúrate de tener este modelo
import { SimulatorService } from '../../services/simulator.service';

@Component({
  selector: 'app-associatenamespacetointerface',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './associatenamespacetointerface.component.html',
  styleUrl: './associatenamespacetointerface.component.css'
})
export class AssociatenamespacetointerfaceComponent implements OnInit {
  @Input() nameSpaceData!: NameSpace;
  @Output() close = new EventEmitter<void>();

  interfaces: Interface[] = [];
  selectedInterfaceId: string = '';
  message: string | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getInterfaces().subscribe({
      next: (data) => this.interfaces = data,
      error: (err) => this.error = 'Error loading interfaces: ' + (err?.message || err)
    });
  }

  associate(): void {
    this.message = null;
    this.error = null;

    if (!this.selectedInterfaceId) {
      this.error = 'Please select an interface.';
      return;
    }

    this.simulatorService.associateNameSpaceToInterface(this.nameSpaceData.id, this.selectedInterfaceId).subscribe({
      next: () => {
        this.message = 'Successfully associated interface to namespace.';
        this.selectedInterfaceId = '';
      },
      error: (err) => {
        this.error = 'Error associating interface: ' + (err?.message || err);
      }
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}
