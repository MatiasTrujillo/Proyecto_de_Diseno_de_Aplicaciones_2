import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';
import { Type } from './Type';

export interface Method {
  name: string;
  stereotype: KeyWord | null;
  accessibility: Accessibility;
  returnType: Type;
  IsOverride: boolean;
}