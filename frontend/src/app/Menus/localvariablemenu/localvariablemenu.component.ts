import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SimulatorService } from '../../services/simulator.service';

@Component({
  selector: 'app-localvariablemenu',
  imports: [],
  templateUrl: './localvariablemenu.component.html',
  styleUrl: './localvariablemenu.component.css'
})
export class LocalvariablemenuComponent {

  constructor(private simulatorService: SimulatorService, private router: Router) {}

    goTo(path: string): void {
    window.location.href = path;
  }
  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }

}
