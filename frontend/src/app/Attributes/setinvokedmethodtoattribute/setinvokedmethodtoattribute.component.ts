import { Component, OnInit } from '@angular/core';
import { SimulatorService } from '../../services/simulator.service'; // Ajustá el path según tu estructura
import { Attribute } from '../../models/attribute'; // Asegurate de tenerlo definido
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-set-invoke-method',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './setinvokedmethodtoattribute.component.html',
  styleUrls: ['./setinvokedmethodtoattribute.component.css']
})
export class SetinvokedmethodtoattributeComponent implements OnInit {
  attributes: Attribute[] = [];
  methods: any[] = [];
  selectedAttributeId: string = '';
  selectedMethodId: string = '';
  message: string = '';
  error: string = '';

  constructor(private apiService: SimulatorService) {}

  ngOnInit() {
    this.apiService.getAttribute().subscribe({
      next: (res) => (this.attributes = res),
      error: (err) => (this.error = 'Error loading attributes')
    });

    this.apiService.getInvokedMethods().subscribe({
      next: (res) => (this.methods = res),
      error: (err) => (this.error = 'Error loading methods')
    });
  }

  associate() {
    if (!this.selectedAttributeId || !this.selectedMethodId) {
      this.error = 'Please select both an attribute and a method.';
      return;
    }

    this.apiService.setInvokeMethodToAttribute(this.selectedAttributeId, this.selectedMethodId).subscribe({
      next: (res) => {
        this.message = res.message;
        this.error = '';
      },
      error: (err) => {
        this.error = 'Failed to associate method to attribute.';
        this.message = '';
      }
    });
  }
}
