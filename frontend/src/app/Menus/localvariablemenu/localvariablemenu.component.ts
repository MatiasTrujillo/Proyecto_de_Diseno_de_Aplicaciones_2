import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { LocalVariable } from '../../models/localvariable';
import { SetinvokedmethodtolocalvariableComponent } from '../../LocalVariale/setinvokedmethodtolocalvariable/setinvokedmethodtolocalvariable.component';  
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-localvariablemenu',
  standalone: true,
  imports: [SetinvokedmethodtolocalvariableComponent, CommonModule],
  templateUrl: './localvariablemenu.component.html',
  styleUrl: './localvariablemenu.component.css'
})
export class LocalvariablemenuComponent {
  error: string | null = null;

  constructor(private simulatorService: SimulatorService, private router: Router) {}

      localVariables: LocalVariable[] = [];
      activeModal: string | null = null;
      selectedLocalVariable: LocalVariable | null = null;


      ngOnInit(): void {
    this.simulatorService.getLocalVariables().subscribe({
      next: (localVariables) => this.localVariables = localVariables,
      error: (err) => this.error = 'Error loading parameters'
    });
  }

  openModal(modalName: string, selectedLocalVariable: LocalVariable): void {
    if (!selectedLocalVariable.id) {
      alert('Local Variable has no id!');
      return;
    }
    this.activeModal = modalName;
    this.selectedLocalVariable = selectedLocalVariable;
  }

      closeModal(): void {
      this.activeModal = null;
    }

    goTo(path: string): void {
    window.location.href = path;
  }
  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }

}
