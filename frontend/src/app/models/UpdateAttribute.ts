import { KeyWord } from './KeyWord';
import { Accessibility } from './Accessibility';

export interface UpdateAttribute {
  name: string | null;
  stereotype: KeyWord | null;
  idType: string | null;
  accessibility: Accessibility | null;
}
