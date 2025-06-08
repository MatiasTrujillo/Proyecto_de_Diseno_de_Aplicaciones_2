import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SimulatorService } from '../services/simulator.service';
import { CreateclassComponent } from '../Classes/createclass/createclass.component';
import { CreateMethodComponent } from '../Methods/createmethod/createmethod.component';
import { CreateMethodSignaturesComponent } from '../MethodSignatures/createSignature/createmethodsignatures.component';
import { CreateattributeComponent } from '../Attributes/createattribute/createattribute.component';
import { CreatelocalvariableComponent } from '../createlocalvariable/createlocalvariable.component';
import { CreateinterfaceComponent } from '../Interfaces/createinterface/createinterface.component';
import { UpdateclassComponent } from '../Classes/updateclass/updateclass.component';
import { DeleteclassComponent } from '../Classes/deleteclass/deleteclass.component';
import { DeleteinterfaceComponent } from '../Interfaces/deleteinterface/deleteinterface.component';
import { UpdateattributeComponent } from '../Attributes/updateattribute/updateattribute.component';
import { AssociateattributewithclassComponent } from '../Attributes/associateattributewithclass/associateattributewithclass.component';
import { DeleteattributeComponent } from '../Attributes/deleteattribute/deleteattribute.component';
import { SetinvokedmethodtoattributeComponent } from '../Attributes/setinvokedmethodtoattribute/setinvokedmethodtoattribute.component';
import { AssociatemethodwithclassComponent } from '../Methods/associatemethodwithclass/associatemethodwithclass.component';
import { DeletemethodComponent } from '../Methods/deletemethod/deletemethod.component';


@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, RouterModule,
    CreateclassComponent,
    CreateMethodComponent,
    CreateMethodSignaturesComponent,
    CreateattributeComponent,
    CreatelocalvariableComponent,
    CreateinterfaceComponent,
    UpdateclassComponent,
    DeleteclassComponent,
    DeleteinterfaceComponent,
    DeleteclassComponent,
    AssociateattributewithclassComponent,
    UpdateattributeComponent,
    DeleteattributeComponent,
    SetinvokedmethodtoattributeComponent,
    AssociatemethodwithclassComponent,
    DeletemethodComponent],
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
