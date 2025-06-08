import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CreateMethodComponent } from '../Methods/createmethod/createmethod.component';
import { AssociatemethodwithclassComponent } from '../Methods/associatemethodwithclass/associatemethodwithclass.component';
import { DeletemethodComponent } from '../Methods/deletemethod/deletemethod.component';
import { SimulatorService } from '../services/simulator.service';
import { Method } from '../models/method';


@Component({
  selector: 'app-methodmenu',
  standalone: true,
  imports: [CommonModule, RouterModule,
    AssociatemethodwithclassComponent,
    DeletemethodComponent],
  templateUrl: './methodmenu.component.html',
  styleUrl: './methodmenu.component.css'
})
export class MethodmenuComponent {

constructor(private simulatorService: SimulatorService, private router: Router) {}

  methods: Method[] = [];
  activeModal: string | null = null;
  selectedMethod: any = null;

 ngOnInit(): void {
    this.simulatorService.getMethods().subscribe({
      next: (data) => this.methods = data,
      error: (err) => console.error('Error loading methods:', err)
    });
  }

  openModal(modalName: string, selectedMethod: any): void {
    this.activeModal = modalName;
    this.selectedMethod = selectedMethod;
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
}
