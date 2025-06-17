import { Type } from './Type';
import { Parameter } from './Parameter';

export interface MethodSignature {
    id: string;
    name: string;
    returnType: Type | null;
    parameters: Parameter[];
}