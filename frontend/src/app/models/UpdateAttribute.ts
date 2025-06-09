import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';

export interface UpdateAttribute {
  name: string | null;
  stereotype: KeyWord | null;
  IdReferenceType: string | null;
  IdInstanceType: string | null;
  accessibility: Accessibility | null;
}
