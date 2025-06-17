import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';
import { UpdateclassComponent } from '../../Classes/updateclass/updateclass.component';
import { DeleteclassComponent } from '../../Classes/deleteclass/deleteclass.component';
import { Class } from '../../models/class';
import { KeyWord } from '../../models/KeyWord';

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
    this.loadClasses();
  }

  loadClasses(): void {
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

  onClassChanged(): void {
    this.closeModal();
    this.loadClasses();
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
}
