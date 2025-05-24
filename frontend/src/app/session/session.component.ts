import { Component } from '@angular/core';
import { SimulatorService } from '../services/simulator.service';
import { Session } from '../models/Session';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-session',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.css'],
})
export class SessionComponent {
  token: string = '';
  message: string = '';
  isError: boolean = false;

  constructor(private sessionService: SimulatorService) {}

  isValidGuid(value: string): boolean {
    const guidRegex = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$/;
    return guidRegex.test(value);
  }

  signIn() {
    if (!this.isValidGuid(this.token)) {
      this.message = 'Invalid GUID format. Please enter a valid token.';
      this.isError = true;
      return;
    }
    
    const session: Session = { token: this.token };
    this.sessionService.signIn(session).subscribe({
      next: (res) => {
        this.token = res.token;
        this.message = 'Signed in. Token saved.';
        this.isError = false;
      },
      error: (err) => {
        this.message = this.getErrorMessage(err);
        this.isError = true;
      },
    });
  }

  logIn() {
    if (!this.isValidGuid(this.token)) {
      this.message = 'Invalid GUID format. Please enter a valid token.';
      this.isError = true;
      return;
    }
    
    this.sessionService.logIn(this.token).subscribe({
      next: (res) => {
        this.message = res.message ?? res;
        this.isError = false;
      },
      error: (err) => {
        this.message = this.getErrorMessage(err);
        this.isError = true;
      },
    });
  }

  logOut() {
    if (!this.isValidGuid(this.token)) {
      this.message = 'Invalid GUID format. Please enter a valid token.';
      this.isError = true;
      return;
    }
    
    this.sessionService.logOut(this.token).subscribe({
      next: (res) => {
        this.message = res.message ?? res;
        this.isError = false;
      },
      error: (err) => {
        this.message = this.getErrorMessage(err);
        this.isError = true;
      },
    });
  }

  private getErrorMessage(error: any): string {
    console.log('Full error object:', error);

    if (error.error) {
      if (typeof error.error === 'string') {
        return error.error;
      } else if (error.error.message) {
        return error.error.message;
      }
    }
    if (error.message) {
      return error.message;
    }
    return 'An unknown error occurred.';
  }
}
