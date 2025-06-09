import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { SimulatorService } from '../../services/simulator.service';
import { Attribute } from '../../models/attribute';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Method } from '../../models/method';
import { InvokedMethod } from '../../models/InvokedMethod';

@Component({
  selector: 'app-set-invoke-method',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './setinvokedmethodtoattribute.component.html',
  styleUrls: ['./setinvokedmethodtoattribute.component.css']
})
export class SetinvokedmethodtoattributeComponent implements OnInit {
  @Input() attributeData!: Attribute;
  @Output() close = new EventEmitter<void>();

  methods: InvokedMethod[] = [];
  selectedMethodId: string = '';
  message: string = '';
  error: string = '';

  constructor(private apiService: SimulatorService) {}

  ngOnInit() {
  this.apiService.getInvokedMethods().subscribe({
    next: (res) => {
      console.log('Métodos invocados:', res);
      this.methods = res;
    },
    error: () => (this.error = 'Error loading methods')
  });
}

  associate() {
    this.message = '';
    this.error = '';

    if (!this.attributeData?.id || !this.selectedMethodId) {
      this.error = 'Please select a method.';
      return;
    }

    this.apiService.setInvokeMethodToAttribute(this.attributeData.id, this.selectedMethodId).subscribe({
      next: (res) => {
        this.message = res.message ?? 'Method associated successfully.';
        this.error = '';
      },
      error: () => {
        this.error = 'Failed to associate method to attribute.';
        this.message = '';
      }
    });
  }
}
