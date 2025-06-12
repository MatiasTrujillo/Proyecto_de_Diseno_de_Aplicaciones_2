import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { Interface } from '../../models/Interface';
import { CommonModule } from '@angular/common';
import { DeleteinterfaceComponent } from '../../Interfaces/deleteinterface/deleteinterface.component';  
import { AssociateInterfaceSignatureComponent } from '../../Interfaces/associate-interface-signature/associate-interface-signature.component';

@Component({
  selector: 'app-interfacemenu',
  standalone: true,
  imports: [CommonModule, DeleteinterfaceComponent, AssociateInterfaceSignatureComponent],
  templateUrl: './interfacemenu.component.html',
  styleUrl: './interfacemenu.component.css'
})
export class InterfacemenuComponent implements OnInit {

  interfaces: Interface[] = [];
  activeModal: string | null = null;
  selectedInterface: Interface | null = null;
  error: string | null = null;

  constructor(private simulatorService: SimulatorService, private router: Router) {}

  ngOnInit(): void {
    this.loadInterfaces();
  }

  loadInterfaces(): void {
    this.simulatorService.getInterfaces().subscribe({
      next: (data) => this.interfaces = data,
      error: () => this.error = 'Error loading interfaces'
    });
  }

  goTo(path: string): void {
    window.location.href = path;
  }

  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }

  openModal(modalName: string, selectedInterface: Interface): void {
    this.activeModal = modalName;
    this.selectedInterface = selectedInterface;
  }

  closeModal(): void {
    this.activeModal = null;
    this.selectedInterface = null;
  }

  onInterfaceChanged(): void {
    this.closeModal();
    this.loadInterfaces();
  }
}
