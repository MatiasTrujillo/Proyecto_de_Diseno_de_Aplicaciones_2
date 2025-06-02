import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
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
  updateForm!: FormGroup;
  message: string | null = null;
  error: string | null = null;
  classes: Class[] = [];
  stereotype: KeyWord | null = null;

  keyWordLabels = Object.keys(KeyWord);
  keyWords = Object.keys(KeyWord).filter(k => isNaN(Number(k)));

  constructor(
    private fb: FormBuilder,
    private simulatorService: SimulatorService
  ) {}

  getKeyValue(label: string): number {
    return KeyWord[label as keyof typeof KeyWord];
  }

  ngOnInit(): void {
    this.updateForm = this.fb.group({
      classToUpdate: [''],
      idClass: [''],
      name: [''],
      stereotype: ['no change'],
    });

    this.simulatorService.getClasses().subscribe({
      next: (res) => this.classes = res,
      error: (err) => console.error('Error fetching classes', err)
    });
  }

  onSubmit(): void {
    const form = this.updateForm.value;

    const updateDto: UpdateClass = {
      name: form.name || null,
      stereotype: form.stereotype || null,
      idClass: form.idClass || null
    };

    this.simulatorService.updateClass(form.classToUpdate, updateDto).subscribe({
    next: () => {
      this.message = 'Class updated successfully';
      this.error = null;
    },
    error: (err) => {
      this.error = 'Error updating class: ' + (err?.error?.message || err.message || 'Unknown error');
      this.message = null;
    }
  });
  }
}
