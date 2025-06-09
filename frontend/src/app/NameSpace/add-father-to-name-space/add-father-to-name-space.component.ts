import { Component } from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';
import { NameSpace } from '../../models/NameSpace';

@Component({
  selector: 'app-add-father-to-name-space',
  imports: [],
  templateUrl: './add-father-to-name-space.component.html',
  styleUrl: './add-father-to-name-space.component.css'
})
export class AddFatherToNameSpaceComponent {
    @Input() nameSpaceData!: NameSpace;
   @Output() close = new EventEmitter<void>();

  message: string | null = null;
  error: string | null = null;

}
