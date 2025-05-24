import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { SimulatorService } from '../services/simulator.service';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(
    private simulatorService: SimulatorService,
    private router: Router
  ) {}

  logout(): void {
  const token = localStorage.getItem('authToken'); // Obtener el token guardado
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
}
