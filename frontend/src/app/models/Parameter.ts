import { Type } from './Type';

export interface Parameter {
  name: string;
  ReferenceType: Type;
  InstanceType: Type;
  id: string;
}