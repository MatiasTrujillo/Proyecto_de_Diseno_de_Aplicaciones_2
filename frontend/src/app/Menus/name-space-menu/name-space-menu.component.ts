import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { NameSpace } from '../../models/NameSpace';
import { CommonModule } from '@angular/common';
import { AssociatenamespacetointerfaceComponent } from '../../NameSpace/associatenamespacetointerface/associatenamespacetointerface.component';
import { AssociatenamespacetoclassComponent } from '../../NameSpace/associatenamespacetoclass/associatenamespacetoclass.component';
import { AddChildToNameSpaceComponent } from '../../NameSpace/add-child-to-name-space/add-child-to-name-space.component';
import { AddFatherToNameSpaceComponent } from '../../NameSpace/add-father-to-name-space/add-father-to-name-space.component';

@Component({
  selector: 'app-name-space-menu',
  standalone: true,
  imports: [RouterModule, CommonModule, AssociatenamespacetointerfaceComponent,AssociatenamespacetoclassComponent, AddChildToNameSpaceComponent, AddFatherToNameSpaceComponent],
  templateUrl: './name-space-menu.component.html',
  styleUrl: './name-space-menu.component.css'
})
export class NameSpaceMenuComponent {
  constructor(private simulatorService: SimulatorService, private router: Router) {}

  nameSpaces: NameSpace[] = [];
  activeModal: string | null = null;
  selectedNameSpace: NameSpace | null = null;

  ngOnInit(): void {
    this.simulatorService.getNameSpaces().subscribe({
      next: (data) => this.nameSpaces = data,
      error: (err) => console.error('Error loading methods:', err)
    });
  }

  openModal(modalName: string, selectedNameSpace: NameSpace): void {
    this.activeModal = modalName;
    this.selectedNameSpace = selectedNameSpace;
  }

  closeModal(): void {
    this.activeModal = null;
  }

  goToClassMenu() {
    this.router.navigate(['/ClassMenuComponent']);
  }

  goToMethodMenu() {
    this.router.navigate(['/MethodMenuComponent']);
  }

  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }

  goToAttributeMenu() {
    this.router.navigate(['/AttributeMenuComponent']);
  }

  goToNameSpace() {
    this.router.navigate(['/NameSpaceMenuComponent']);
  }
}
