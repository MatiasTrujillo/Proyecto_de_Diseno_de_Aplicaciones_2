import { Type } from './Type';

export interface LocalVariable {
  name: string;
  ReferenceType: Type;
  InstanceType: Type;
  id: string;
}