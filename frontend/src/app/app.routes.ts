import { Routes } from '@angular/router';
import { SessionComponent } from './session/session.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CreateclassComponent } from './Classes/createclass/createclass.component';
import { CreateMethodComponent } from './Methods/createmethod/createmethod.component';
import { CreateattributeComponent } from './Attributes/createattribute/createattribute.component';
import { CreatelocalvariableComponent } from './createlocalvariable/createlocalvariable.component';
import { CreateinterfaceComponent } from './Interfaces/createinterface/createinterface.component';
import { ClassMenuComponent } from './class-menu/class-menu.component';
import { MethodmenuComponent } from './methodmenu/methodmenu.component';

export const routes: Routes = [
  { path: '', component: SessionComponent },
  { path: 'navMenu', component: NavMenuComponent },
  { path: 'createClass', component: CreateclassComponent },
  { path: 'createInterface', component: CreateinterfaceComponent },
  { path: 'createMethod', component: CreateMethodComponent },
  { path: 'createAttribute', component: CreateattributeComponent },
  { path: 'createLocalVariable', component: CreatelocalvariableComponent },
  { path: 'ClassMenuComponent', component: ClassMenuComponent },
  { path: 'MethodMenuComponent', component: MethodmenuComponent },
  { path: '**', redirectTo: '' }
];