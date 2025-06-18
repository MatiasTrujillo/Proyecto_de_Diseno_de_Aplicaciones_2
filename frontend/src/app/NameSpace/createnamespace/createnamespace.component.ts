import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { NameSpace } from '../../models/NameSpace';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-createnamespace',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './createnamespace.component.html',
  styleUrls: ['./createnamespace.component.css'],
})
export class CreatenamespaceComponent {
  namespaceForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {
    this.namespaceForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

 onSubmit(): void {
  if (this.namespaceForm.valid) {
    const namespace: NameSpace = {
      id: '',
      name: this.namespaceForm.value.name,
    };

    this.simulatorService.createNameSpace(namespace).subscribe({
      next: (response) => {
        this.successMessage = `Namespace creado con ID: ${response.id}`;
        this.errorMessage = '';
        this.namespaceForm.reset();
        this.clearMessagesAfterDelay();
      },
      error: (error) => {
        this.errorMessage = error?.error?.message || 'Error al crear el namespace';
        this.successMessage = '';
        console.error(error);
        this.clearMessagesAfterDelay();
      },
    });
  }
}

private clearMessagesAfterDelay(): void {
  setTimeout(() => {
    this.successMessage = '';
    this.errorMessage = '';
  }, 10000);
}
}
