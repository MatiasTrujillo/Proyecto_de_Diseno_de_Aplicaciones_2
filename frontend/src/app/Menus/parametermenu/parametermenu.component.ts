import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { Parameter } from '../../models/Parameter';
import { CommonModule } from '@angular/common';
import { SetInvokeMethodToParameterComponent } from '../../Parameter/set-invoke-method-to-parameter/set-invoke-method-to-parameter.component';

@Component({
  selector: 'app-parametermenu',
  standalone: true,
  imports: [CommonModule, SetInvokeMethodToParameterComponent],
  templateUrl: './parametermenu.component.html',
  styleUrl: './parametermenu.component.css'
})
export class ParametermenuComponent implements OnInit {
  error: string | null = null;

  constructor(private router: Router, private simulatorService: SimulatorService) {}

    parameters: Parameter[] = [];
    activeModal: string | null = null;
    selectedParameter: Parameter | null = null;

  ngOnInit(): void {
    this.simulatorService.getParameters().subscribe({
      next: (params) => this.parameters = params,
      error: (err) => this.error = 'Error loading parameters'
    });
  }

openModal(modalName: string, selectedParameter: Parameter): void {
  if (!selectedParameter.id) {
    alert('Parameter has no id!');
    return;
  }
  this.activeModal = modalName;
  this.selectedParameter = selectedParameter;
}
  
    closeModal(): void {
    this.activeModal = null;
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

  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }
  goTo(path: string): void {
    window.location.href = path;
  }
}