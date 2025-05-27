import { Routes } from '@angular/router';
import { SessionComponent } from './session/session.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CreateclassComponent } from './createclass/createclass.component';

export const routes: Routes = [
  { path: '', component: SessionComponent },
  { path: 'navMenu', component: NavMenuComponent },
  { path: 'createClass', component: CreateclassComponent },
];