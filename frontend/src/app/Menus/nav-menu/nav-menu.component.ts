import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { CreateclassComponent } from '../../Classes/createclass/createclass.component';
import { CreateMethodComponent } from '../../Methods/createmethod/createmethod.component';
import { CreateattributeComponent } from '../../Attributes/createattribute/createattribute.component';
import { CreatelocalvariableComponent } from '../../LocalVariale/createlocalvariable/createlocalvariable.component';
import { CreateinterfaceComponent } from '../../Interfaces/createinterface/createinterface.component';
import { DeleteinterfaceComponent } from '../../Interfaces/deleteinterface/deleteinterface.component';
import { CreatenamespaceComponent } from '../../NameSpace/createnamespace/createnamespace.component';
import { CreateMethodSignaturesComponent } from '../../MethodSignatures/createSignature/createmethodsignatures.component';
import { CreateparameterComponent } from '../../Parameter/createparameter/createparameter.component';
import { CreateinvokedmethodComponent } from '../../createinvokedmethod/createinvokedmethod.component';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, RouterModule,
    CreateclassComponent,
    CreateMethodComponent,
    CreateattributeComponent,
    CreatelocalvariableComponent,
    CreateinterfaceComponent,
    DeleteinterfaceComponent,
    CreatenamespaceComponent,
    CreateMethodSignaturesComponent,
    CreateparameterComponent,
    CreateinvokedmethodComponent
],
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
activeModal: string | null = null;

openModal(modalName: string): void {
  this.activeModal = modalName;
}

closeModal(): void {
  this.activeModal = null;
}
  goTo(path: string): void {
    window.location.href = path;
  }
}