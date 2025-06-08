import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';
import { Type } from './Type';

export interface Method {
  id: string;
  name: string;
  stereotype: KeyWord | null;
  accessibility: Accessibility;
  returnType: Type;
  IsOverride: boolean;
}