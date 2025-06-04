import { Component, OnInit } from '@angular/core';
import { SimulatorService } from '../../services/simulator.service';
import { Method } from '../../models/method';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, NgIf, NgFor } from '@angular/common';

@Component({
  selector: 'app-deletemethod',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgIf, NgFor],
  templateUrl: './deletemethod.component.html',
  styleUrls: ['./deletemethod.component.css']
})
export class DeletemethodComponent implements OnInit {
  methods: Method[] = [];
  deleteForm: FormGroup;
  message = '';
  errorMessage = '';

  constructor(
    private simulatorService: SimulatorService,
    private fb: FormBuilder
  ) {
    this.deleteForm = this.fb.group({
      methodId: ['']
    });
  }

  ngOnInit(): void {
    this.loadMethods();
  }

  loadMethods(): void {
    this.simulatorService.getMethods().subscribe({
      next: (methods) => {
        this.methods = methods;
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar los métodos.';
        console.error(error);
      }
    });
  }

  onDelete(): void {
    const methodId = this.deleteForm.value.methodId;
    if (!methodId) return;

    this.simulatorService.deleteMethod(methodId).subscribe({
      next: () => {
        this.message = 'Método eliminado correctamente.';
        this.errorMessage = '';
        this.methods = this.methods.filter(m => m.id !== methodId);
        this.deleteForm.reset();
      },
      error: (error) => {
        this.errorMessage = 'Error al eliminar el método.';
        this.message = '';
        console.error(error);
      }
    });
  }
}
