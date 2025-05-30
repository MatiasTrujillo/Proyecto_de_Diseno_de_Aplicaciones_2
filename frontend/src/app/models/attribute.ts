import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';
import { Type } from './Type';

export interface Attribute {
  name: string;
  accessibility: Accessibility;
  type: Type;
  stereotype: KeyWord | null;
}