import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AssociatemethodwithclassComponent } from '../../Methods/associatemethodwithclass/associatemethodwithclass.component';
import { DeletemethodComponent } from '../../Methods/deletemethod/deletemethod.component';
import { SimulatorService } from '../../services/simulator.service';
import { Method } from '../../models/method';
import { KeyWord } from '../../models/KeyWord';
import { Accessibility } from '../../models/Accessibility';
import { AddmethodComponent } from '../../Methods/addmethod/addmethod.component';
import { AssociatemethodwhitLocalVariableComponent } from '../../Methods/associatemethodwhit-local-variable/associatemethodwhit-local-variable.component';
import { AssociatemethodwhitParameterComponent } from '../../Methods/associatemethodwhit-parameter/associatemethodwhit-parameter.component';

@Component({
  selector: 'app-methodmenu',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    AssociatemethodwithclassComponent,
    DeletemethodComponent,
    AddmethodComponent,
    AssociatemethodwhitLocalVariableComponent,
    AssociatemethodwhitParameterComponent
  ],
  templateUrl: './methodmenu.component.html',
  styleUrl: './methodmenu.component.css'
})
export class MethodmenuComponent implements OnInit {

  methods: Method[] = [];
  activeModal: string | null = null;
  selectedMethod: Method | null = null;

  constructor(private simulatorService: SimulatorService, private router: Router) {}

  ngOnInit(): void {
    this.loadMethods();
  }

  loadMethods(): void {
  const excludedNames = [
    "ToString",
    "Equals",
    "GetHashCode",
    "Parse",
    "MaxValue",
    "MinValue",
    "CompareTo",
    "IsNullOrEmpty",
    "IsNullOrWhiteSpace",
    "ToLowerCase",
    "ToUpperCase"
  ];

  this.simulatorService.getMethods().subscribe({
    next: (data) => {
      this.methods = data.filter(m => !excludedNames.includes(m.name));
    },
    error: (err) => console.error('Error loading methods:', err)
  });
}

  openModal(modalName: string, selectedMethod: Method): void {
    this.activeModal = modalName;
    this.selectedMethod = selectedMethod;
  }

  closeModal(): void {
    this.activeModal = null;
    this.selectedMethod = null;
  }

  onMethodChanged(): void {
    this.closeModal();
    this.loadMethods();
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

  goTo(path: string): void {
    window.location.href = path;
  }

  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }

  getStereotypeName(value: number | null | undefined): string {
    if (value === null || value === undefined) return '';
    return KeyWord[value] ?? '';
  }

    getAccesibilityName(value: number | null | undefined): string {
    if (value === null || value === undefined) return '';
    return Accessibility[value] ?? '';
  }
}
