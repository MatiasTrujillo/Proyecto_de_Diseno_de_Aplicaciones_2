import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';
import { Type } from './Type';

export interface Attribute {
  id: string;
  name: string;
  accessibility: Accessibility;
  referenceType: Type;
  instanceType: Type | null;
  stereotype: KeyWord | null;
}