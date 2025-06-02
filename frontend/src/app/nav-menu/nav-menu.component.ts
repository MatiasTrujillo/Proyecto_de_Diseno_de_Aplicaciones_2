import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SimulatorService } from '../services/simulator.service';
import { CreateclassComponent } from '../Classes/createclass/createclass.component';
import { CreateMethodComponent } from '../Methods/createmethod/createmethod.component';
import { CreateattributeComponent } from '../Attributes/createattribute/createattribute.component';
import { CreatelocalvariableComponent } from '../createlocalvariable/createlocalvariable.component';
import { CreateinterfaceComponent } from '../Interfaces/createinterface/createinterface.component';
import { UpdateclassComponent } from '../Classes/updateclass/updateclass.component';
import { DeleteclassComponent } from '../Classes/deleteclass/deleteclass.component';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, RouterModule,
    CreateclassComponent,
    CreateMethodComponent,
    CreateattributeComponent,
    CreatelocalvariableComponent,
    CreateinterfaceComponent, 
    UpdateclassComponent,
    DeleteclassComponent ],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(
    private simulatorService: SimulatorService,
    private router: Router
  ) {}

  logout(): void {
  const token = localStorage.getItem('authToken');
  if (!token) {
    console.error('No token found in localStorage.');
    return;
  }

  this.simulatorService.logOut(token).subscribe(
    () => {
      localStorage.removeItem('authToken');
      localStorage.removeItem('roles');
      this.router.navigate(['']);
    },
    (error) => {
      console.error('Error logging out', error);
    }
  );
}

activeModal: string | null = null;

openModal(modalName: string): void {
  this.activeModal = modalName;
}

closeModal(): void {
  this.activeModal = null;
}

}
