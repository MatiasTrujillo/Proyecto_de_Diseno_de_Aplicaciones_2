import { Type } from './Type';

export interface TransformerForExporter {
    ReferenceId: string | null;
    ObjectToCreateId: string | null;
    MethodId: string | null;
    ParameterTypes: Type[] | null;
}