import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SimulatorService } from '../../services/simulator.service';
import { Method } from '../../models/method';
import { Class } from '../../models/class';
import { Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-associatemethodwithclass',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './associatemethodwithclass.component.html',
  styleUrl: './associatemethodwithclass.component.css'
})
export class AssociatemethodwithclassComponent implements OnInit {
  @Input() methodData!: Method;
  @Output() close = new EventEmitter<void>();
  associateForm: FormGroup;
  methods: Method[] = [];
  classes: Class[] = [];
  message = '';
  error = '';

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {
    this.associateForm = this.fb.group({
      methodId: [''],
      classId: ['']
    });
  }

  ngOnInit(): void {
    this.loadMethods();
    this.loadClasses();
  }

  loadMethods(): void {
    this.simulatorService.getMethods().subscribe({
      next: (methods) => (this.methods = methods),
      error: () => (this.error = 'Error al cargar los métodos')
    });
  }

  loadClasses(): void {
    this.simulatorService.getClasses().subscribe({
      next: (classes) => (this.classes = classes),
      error: () => (this.error = 'Error al cargar las clases')
    });
  }

  onAssociate(): void {
  const methodId = this.methodData?.id;
  const classId = this.associateForm.value.classId;

  if (!methodId || !classId) {
    this.error = 'Debe seleccionar una clase válida.';
    return;
  }

  this.simulatorService.associateMethodWithClass(methodId, classId).subscribe({
    next: (res) => {
      this.message = res.message;
      this.error = '';
    },
    error: (err) => {
      this.message = '';
      this.error = 'Error al asociar el método a la clase';
      console.error(err);
    }
  });
}
}