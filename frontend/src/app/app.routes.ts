import { Routes } from '@angular/router';
import { CreateClassComponent } from './create-class/create-class.component';
import { EditClassComponent } from './edit-class/edit-class.component';

export const routes: Routes = [
  { path: 'createClass', component: CreateClassComponent },
  { path: 'editClass', component: EditClassComponent }
];
