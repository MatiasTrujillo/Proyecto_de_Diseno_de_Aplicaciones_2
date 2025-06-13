import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { SimulatorService } from '../../services/simulator.service';
import { Class } from '../../models/class';
import { UpdateClass } from '../../models/updateClass';
import { KeyWord } from '../../models/KeyWord';

@Component({
  selector: 'app-updateclass',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './updateclass.component.html',
  styleUrls: ['./updateclass.component.css']
})
export class UpdateclassComponent implements OnInit {
  @Input() classData: Class | null = null;
  @Output() close = new EventEmitter<void>();

  updateForm!: FormGroup;
  message: string | null = null;
  error: string | null = null;

  keyWords = Object.keys(KeyWord).filter(k => isNaN(Number(k)));
  classes: Class[] = [];

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {}

  ngOnInit(): void {
    this.updateForm = this.fb.group({
      classToUpdate: [this.classData?.id || ''],
      idClass: [this.classData?.id || null],
      name: [this.classData?.name || ''],
      stereotype: ['no change']
    });

    this.simulatorService.getClasses().subscribe({
      next: (res) => {
        this.classes = res.filter(c => c.id !== this.classData?.id);
        const objectClass = this.classes.find(c => c.name === 'Object');
        if (objectClass) {
          this.updateForm.patchValue({ idClass: objectClass.id });
        }
      },
      error: (err) => {
        this.error = 'Error loading classes: ' + (err.message || 'Unknown error');
      }
    });
  }

  onSubmit(): void {
    const form = this.updateForm.value;

    const updateDto: UpdateClass = {
      name: form.name || null,
      stereotype: form.stereotype !== 'no change' ? form.stereotype : null,
      idClass: form.idClass || null
    };

    this.simulatorService.updateClass(form.classToUpdate, updateDto).subscribe({
      next: () => {
        this.message = 'Class updated successfully';
        this.error = null;
        this.close.emit(); // cerrar modal
      },
      error: (err) => {
        this.error = 'Error updating class: ' + (err?.error?.message || err.message || 'Unknown error');
        this.message = null;
      }
    });
  }
}
