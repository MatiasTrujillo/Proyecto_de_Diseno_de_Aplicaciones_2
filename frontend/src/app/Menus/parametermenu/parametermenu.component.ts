import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-parametermenu',
  imports: [],
  templateUrl: './parametermenu.component.html',
  styleUrl: './parametermenu.component.css'
})
export class ParametermenuComponent {

  constructor(private router: Router) {}
  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }
    goTo(path: string): void {
    window.location.href = path;
  }
}
