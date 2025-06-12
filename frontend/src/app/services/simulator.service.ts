import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../Environment/environment';
import { Session } from '../models/Session';
import { Class } from '../models/class';
import { Type } from '../models/Type';
import { Method } from '../models/method';
import { MethodSignature } from '../models/MethodSignature';
import { Attribute } from '../models/attribute';
import { LocalVariable } from '../models/localvariable';
import { Interface } from '../models/Interface';
import { UpdateClass } from '../models/updateClass';
import { UpdateAttribute } from '../models/UpdateAttribute';
import { InvokedMethod } from '../models/InvokedMethod';
import { TransformerForExporter } from '../models/TransformerForExporter';
import { NameSpace } from '../models/NameSpace';
import { NameSpaceComponent } from '../models/NameSpaceComponent';
import { Parameter } from '../models/Parameter';

@Injectable({
  providedIn: 'root'
})
export class SimulatorService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  signIn(session: Session): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/sessions`, session).pipe(
      tap((res) => {
        localStorage.setItem('authToken', res.token);
      }),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  logIn(token?: string): Observable<{ message: string }> {
    const resolvedToken = token ?? localStorage.getItem('authToken');
    if (!resolvedToken) {
      return throwError(() => new Error('Token not found in localStorage.'));
    }

    return this.http.patch<{ message: string }>(`${this.apiUrl}/sessions/logins/${resolvedToken}`, null);
  }

  logOut(token?: string): Observable<{ message: string }> {
    const resolvedToken = token ?? localStorage.getItem('authToken');
    if (!resolvedToken) {
      return throwError(() => new Error('Token not found in localStorage.'));
    }

    return this.http.patch<{ message: string }>(`${this.apiUrl}/sessions/logout/${resolvedToken}`, null);
  }

  createClass(classData: Class): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/classes`, classData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  updateClass(id: string, updatedClass: Partial<UpdateClass>): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiUrl}/classes/${id}`, updatedClass).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  deleteClass(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/classes/${id}`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  getClasses(): Observable<Class[]> {
    return this.http.get<Class[]>(`${this.apiUrl}/classes`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  getTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(`${this.apiUrl}/types`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  getInvokedMethods(): Observable<InvokedMethod[]> {
    return this.http.get<InvokedMethod[]>(`${this.apiUrl}/invokedMethods`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getAttribute(): Observable<Attribute[]> {
    return this.http.get<Attribute[]>(`${this.apiUrl}/attributes`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  setInvokeMethodToAttribute(attributeId: string, methodId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.apiUrl}/attributes/${attributeId}/invokedMethods/${methodId}`, null).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createMethod(method: Method): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/methods`, method).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createAttribute(attribute: Attribute): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/attributes`, attribute).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createLocalVariable(localVariable: LocalVariable): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/localVariables`, localVariable).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  createInterface(interfaceData: Interface): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/interfaces`, interfaceData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createMethodSignature(methodSignature: MethodSignature): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/methodSignatures`, methodSignature).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  getInterfaces(): Observable<Interface[]> {
    return this.http.get<Interface[]>(`${this.apiUrl}/interfaces`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  deleteInterface(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/interfaces/${id}`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  associateMethodWithClass(methodId: string, classId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.apiUrl}/methods/${methodId}/classes/${classId}`,
      null
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  addAMethod(methodId: string, invokedMethodId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.apiUrl}/methods/${methodId}/invokedMethods/${invokedMethodId}`,
      null
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  deleteMethod(methodId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/methods/${methodId}`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getMethods(): Observable<Method[]> {
    return this.http.get<Method[]>(`${this.apiUrl}/methods`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getMethodSignatures(): Observable<MethodSignature[]> {
    return this.http.get<MethodSignature[]>(`${this.apiUrl}/methodSignatures`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  updateAttribute(id: string, updatedAttribute: Partial<UpdateAttribute>): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiUrl}/attributes/${id}`, updatedAttribute).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  associateAttributeWithClass(attributeId: string, classId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.apiUrl}/attributes/${attributeId}/classes/${classId}`,
      null
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  deleteAttribute(attributeId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/attributes/${attributeId}`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getExporters(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/exporters`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  exportWithExporter(exporterName: string, data: TransformerForExporter): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/exporters/${exporterName}/export`, data).pipe(
      catchError((error) => throwError(() => error))
    );
  }
private nameSpaceApiUrl = `${this.apiUrl}/namespaces`;

  createNameSpace(nameSpace: NameSpace): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.nameSpaceApiUrl}`, nameSpace).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  associateNameSpaceToClass(nameSpaceId: string, classId: string): Observable<void> {
    return this.http.post<void>(`${this.nameSpaceApiUrl}/${nameSpaceId}/associateclass/${classId}`, null).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  associateNameSpaceToInterface(nameSpaceId: string, interfaceId: string): Observable<void> {
    return this.http.post<void>(`${this.nameSpaceApiUrl}/${nameSpaceId}/associateinterfaces/${interfaceId}`, null).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  addFatherToNameSpace(id: string, idFather: string): Observable<void> {
    return this.http.post<void>(`${this.nameSpaceApiUrl}/${id}/addfathers/${idFather}`, null).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  removeNameSpace(id: string): Observable<void> {
    return this.http.delete<void>(`${this.nameSpaceApiUrl}/${id}`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  addChildToNameSpace(parentId: string, childId: string): Observable<void> {
    return this.http.post<void>(`${this.nameSpaceApiUrl}/${parentId}/addchilds/${childId}`, null).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  removeChildFromNameSpace(parentId: string, childId: string): Observable<void> {
    return this.http.delete<void>(`${this.nameSpaceApiUrl}/${parentId}/removechilds/${childId}`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getNameSpaces(): Observable<NameSpace[]> {
    return this.http.get<NameSpace[]>(`${this.nameSpaceApiUrl}`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getNameSpaceComponents(): Observable<NameSpaceComponent[]> {
    return this.http.get<NameSpaceComponent[]>(`${this.apiUrl}/namespaceComponents`).pipe(
      catchError((error) => throwError(() => error))
    );
  }

private parameterApiUrl = `${this.apiUrl}/parameters`;

  createParameter(parameter: Parameter): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.parameterApiUrl}`, parameter).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  setInvokeMethodToParameter(parameterId: string, invokedMethodId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.parameterApiUrl}/${parameterId}/invokedMethods/${invokedMethodId}`,
      null
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }
private localsApiUrl = `${this.apiUrl}/localVariables`;

    setInvokeMethodToLocalVariable(localVariableId: string, invokedMethodId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.localsApiUrl}/${localVariableId}/invokedMethods/${invokedMethodId}`,
      null
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  createInvokeMethod(invokedMethod: InvokedMethod): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(
      `${this.apiUrl}/invokedMethods`,
      invokedMethod
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  getParameters(): Observable<Parameter[]> {
    return this.http.get<Parameter[]>(`${this.apiUrl}/parameters`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

   getLocalVariables(): Observable<LocalVariable[]> {
    return this.http.get<LocalVariable[]>(`${this.apiUrl}/localVariables`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  associateInterfaceSignature(interfaceId: string, methodSignatureId: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(
      `${this.apiUrl}/interfaces/${interfaceId}/methodSignatures/${methodSignatureId}`,
      null
    ).pipe(
      catchError((error) => throwError(() => error))
    );
  }
}
