import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SimulatorService } from '../services/simulator.service';
import { TransformerForExporter } from '../models/TransformerForExporter';
import { Type } from '../models/Type';
import { Method } from '../models/method';
import { Router } from '@angular/router';

@Component({
  selector: 'app-exportdata',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './exportdata.component.html',
  styleUrls: ['./exportdata.component.css']
})
export class ExportdataComponent implements OnInit {
  exporterName: string = '';
  referenceId: string | null = null;
  objectToCreateId: string | null = null;
  methodId: string | null = null;
  selectedParameterTypes: string[] = [];

  types: Type[] = [];
  methods: Method[] = [];

  result: any = null;
  error: string = '';

  constructor(private simulatorService: SimulatorService, private router: Router) {}

  ngOnInit() {
    this.simulatorService.getTypes().subscribe({
      next: (res) => this.types = res,
      error: () => this.error = 'Failed to load types'
    });

    this.simulatorService.getMethods().subscribe({
      next: (res) => this.methods = res,
      error: () => this.error = 'Failed to load methods'
    });
  }

exportData() {
  this.error = '';
  this.result = null;

  const parameterTypes: Type[] = this.types.filter(t => this.selectedParameterTypes.includes(t.id));

  const data: TransformerForExporter = {
    ReferenceId: this.referenceId,
    ObjectToCreateId: this.objectToCreateId,
    MethodId: this.methodId,
    ParameterTypes: parameterTypes
  };

  this.simulatorService.exportWithExporter(this.exporterName, data).subscribe({
    next: (res) => {
      console.log('Respuesta exportWithExporter:', res); // <-- Aquí ves la respuesta en consola
      this.result = res;
    },
    error: (err) => {
      this.error = err?.error?.message || 'Error desconocido al exportar datos';
    }
  });
}

  goTo(path: string): void {
    window.location.href = path;
  }
  goToNavMenu() {
    this.router.navigate(['/navMenu']);
  }
}
