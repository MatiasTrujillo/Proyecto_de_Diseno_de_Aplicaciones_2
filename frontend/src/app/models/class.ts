import { KeyWord } from './KeyWord';

export interface Class {
  name: string;
  stereotype: KeyWord | null;
  id: string | null;
}