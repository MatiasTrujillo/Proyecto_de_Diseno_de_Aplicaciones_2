import { Routes } from '@angular/router';
import { SessionComponent } from './session/session.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

export const routes: Routes = [
  { path: '', component: SessionComponent },
  { path: 'navMenu', component: NavMenuComponent },
];