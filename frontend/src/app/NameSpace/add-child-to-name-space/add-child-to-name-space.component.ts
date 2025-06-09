import { Component } from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';
import { NameSpace } from '../../models/NameSpace';

@Component({
  selector: 'app-add-child-to-name-space',
  standalone: true,
  imports: [],
  templateUrl: './add-child-to-name-space.component.html',
  styleUrl: './add-child-to-name-space.component.css'
})
export class AddChildToNameSpaceComponent {
   @Input() nameSpaceData!: NameSpace;
   @Output() close = new EventEmitter<void>();

  message: string | null = null;
  error: string | null = null;

}
