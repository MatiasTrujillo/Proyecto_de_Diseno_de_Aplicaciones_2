import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { UpdateclassComponent } from '../../Classes/updateclass/updateclass.component';
import { DeleteclassComponent } from '../../Classes/deleteclass/deleteclass.component';
import { Router } from '@angular/router';
import { Class } from '../../models/class';

@Component({
  selector: 'app-class-menu',
  standalone: true,
  imports: [CommonModule, RouterModule, UpdateclassComponent, DeleteclassComponent],
  templateUrl: './class-menu.component.html',
  styleUrl: './class-menu.component.css'
})
export class ClassMenuComponent implements OnInit {

  classes: Class[] = [];
  activeModal: string | null = null;
  selectedClass: Class | null = null;

  constructor(private simulatorService: SimulatorService, private router: Router) {}

  ngOnInit(): void {
    this.simulatorService.getClasses().subscribe({
      next: (data) => this.classes = data,
      error: (err) => console.error('Error loading classes:', err)
    });
  }

  openModal(modalName: string, selectedClass: Class): void {
    this.activeModal = modalName;
    this.selectedClass = selectedClass;
  }

  closeModal(): void {
    this.activeModal = null;
    this.selectedClass = null; 
  }

  goTo(path: string): void {
    window.location.href = path;
  }
  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }
}
