import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { DeleteattributeComponent } from '../../Attributes/deleteattribute/deleteattribute.component';
import { SimulatorService } from '../../services/simulator.service';
import { Attribute } from '../../models/attribute';
import { UpdateattributeComponent } from "../../Attributes/updateattribute/updateattribute.component";
import { AssociateattributewithclassComponent } from '../../Attributes/associateattributewithclass/associateattributewithclass.component';
import { SetinvokedmethodtoattributeComponent } from '../../Attributes/setinvokedmethodtoattribute/setinvokedmethodtoattribute.component';

@Component({
  selector: 'app-attributemenu',
  standalone: true,
  imports: [
    CommonModule, RouterModule,
    DeleteattributeComponent, UpdateattributeComponent, AssociateattributewithclassComponent, SetinvokedmethodtoattributeComponent
  ],
  templateUrl: './attributemenu.component.html',
  styleUrls: ['./attributemenu.component.css']
})
export class AttributemenuComponent {
  attributes: Attribute[] = [];
  activeModal: string | null = null;
  selectedAttribute: Attribute | null = null;
  result: string = '';
  error: string | null = null;

  constructor(private simulatorService: SimulatorService, private router: Router) {}

  ngOnInit(): void {
    this.loadAttributes();
  }

  loadAttributes(): void {
    this.simulatorService.getAttribute().subscribe(
      (res: Attribute[]) => {
        this.attributes = res;
        this.error = null;
      },
      (err) => {
        this.error = 'Error loading attributes: ' + (err?.error?.message || err.message || 'Unknown error');
      }
    );
  }

  openModal(modalName: string, selectedAttribute: Attribute): void {
    this.activeModal = modalName;
    this.selectedAttribute = selectedAttribute;
  }

  closeModal(): void {
    this.activeModal = null;
    this.selectedAttribute = null;
  }

  onAttributeChanged(): void {
    this.closeModal();
    this.loadAttributes();
  }

  goTo(path: string): void {
    window.location.href = path;
  }

  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }

  logout(): void {
  const token = localStorage.getItem('authToken');
  if (!token) {
    console.error('No token found in localStorage.');
    this.router.navigate(['**']);
    return;
  }

  this.simulatorService.logOut(token).subscribe(
    () => {
      localStorage.removeItem('authToken');
      localStorage.removeItem('roles');
      this.router.navigate(['**']);
    },
    (error) => {
      console.error('Error logging out', error);
      this.router.navigate(['**']);
    }
  );
}
}
