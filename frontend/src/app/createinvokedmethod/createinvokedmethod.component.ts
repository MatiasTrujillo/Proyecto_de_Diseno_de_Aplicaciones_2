import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../services/simulator.service';
import { Method } from '../models/method';
import { InvokedMethod } from '../models/InvokedMethod';

@Component({
  selector: 'app-createinvokedmethod',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './createinvokedmethod.component.html',
  styleUrls: ['./createinvokedmethod.component.css']
})
export class CreateinvokedmethodComponent implements OnInit {
  methods: Method[] = [];
  selectedMethodId: string | null = null;
  name: string = '';
  message: string = '';
  error: string = '';

  constructor(private simulatorService: SimulatorService) {}

  ngOnInit(): void {
    this.simulatorService.getMethods().subscribe({
      next: (methods) => this.methods = methods,
      error: () => this.error = 'Error loading methods'
    });
  }

  createInvokedMethod(): void {
    this.message = '';
    this.error = '';

    if (!this.name || !this.selectedMethodId) {
      this.error = 'Name and Method are required.';
      return;
    }

    const invokedMethod: InvokedMethod = {
      referenceName: this.name,
      methodID: this.selectedMethodId,
      id: null
    };

    this.simulatorService.createInvokeMethod(invokedMethod).subscribe({
      next: (res) => {
        this.message = 'Invoked Method created! ID: ' + res.id;
        this.name = '';
        this.selectedMethodId = null;
      },
      error: (err) => {
        this.error = err?.error?.message || 'Error creating invoked method';
      }
    });
  }
}