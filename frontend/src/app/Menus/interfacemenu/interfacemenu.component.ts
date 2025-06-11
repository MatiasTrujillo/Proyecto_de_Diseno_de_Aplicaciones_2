import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';

@Component({
  selector: 'app-interfacemenu',
  imports: [],
  templateUrl: './interfacemenu.component.html',
  styleUrl: './interfacemenu.component.css'
})
export class InterfacemenuComponent {

  constructor(private simulatorService: SimulatorService, private router: Router) {}
  goTo(path: string): void {
    window.location.href = path;
  }
  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }
}
