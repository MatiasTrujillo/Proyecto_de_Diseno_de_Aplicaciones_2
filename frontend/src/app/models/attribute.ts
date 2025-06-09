import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';
import { Type } from './Type';

export interface Attribute {
  id: string;
  name: string;
  accessibility: Accessibility;
  ReferenceType: Type;
  InstanceType: Type;
  stereotype: KeyWord | null;
}