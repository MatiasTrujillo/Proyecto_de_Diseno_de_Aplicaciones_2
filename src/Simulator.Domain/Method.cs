namespace Simulator.Domain;

public class Method
{
    private static int _indentacion = 0;
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Accessibility Accessibility { get; set; }
    public List<Method> Methods { get; set; }
    public List<Parameter> Parameters { get; set; }
    public List<LocalVariable> LocalVariables { get; set; }
    public KeyWord? Stereotype { get; set; }
    public Type ReturnType { get; set; }

    public Method()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Accessibility = Accessibility.Public;
        Methods = [];
        Parameters = [];
        LocalVariables = [];
        Stereotype = null;
        ReturnType = new Primitive();
    }

    public Method(string name, Accessibility accessibility, KeyWord? stereotype, List<Method> methods,
        List<Parameter> parameters,
        List<LocalVariable> localVariables, Type returnType)
    {
        Id = Guid.NewGuid();
        Name = name;
        Accessibility = accessibility;
        Methods = methods;
        Parameters = parameters;
        LocalVariables = localVariables;
        Stereotype = stereotype;
        ReturnType = returnType;
    }

    public string ExecuteMethod(Class reference, Class objectToCreate, Method methodReference)
    {
        _indentacion = 0;
        var result = new List<string>();
        var flag = false;
        if(objectToCreate.HierarchyClass != null)
        {
            flag = reference.Id != objectToCreate.HierarchyClass.Id;
        }

        FirstLine(reference, objectToCreate, methodReference, flag, result);

        foreach(var calledMethod in methodReference.Methods)
        {
            foreach(var param in methodReference.Parameters)
            {
                if(param.ParameterType is Class paramClass)
                {
                    if(paramClass.Methods.Any(m => m.Name == calledMethod.Name))
                    {
                        var indentation = new string('\t', _indentacion);
                        result.Add(
                            $"{indentation}{param.Name}.{calledMethod.Name}() -> {paramClass.Name}.{calledMethod.Name}()");
                    }
                }
            }

            if(reference is Class referenceClass)
            {
                var indentation = new string('\t', _indentacion);
                foreach(var attr in referenceClass.Attributes)
                {
                    if(attr.Type is Class attrClass)
                    {
                        if(attrClass.Methods.Any(m => m.Name == calledMethod.Name))
                        {
                            result.Add(
                                $"{indentation}{attr.Name}.{calledMethod.Name}() -> {attrClass.Name}.{calledMethod.Name}()");
                            break;
                        }

                        var parent = attrClass.HierarchyClass as Class;
                        while(parent != null)
                        {
                            var methodInParent = parent.Methods.FirstOrDefault(m => m.Name == calledMethod.Name);
                            if(methodInParent != null)
                            {
                                result.Add(
                                    $"{indentation}{attr.Name}.{calledMethod.Name}() -> {parent.Name}.{methodInParent.Name}()");
                                break;
                            }

                            parent = (Class)parent.HierarchyClass;
                        }
                    }
                }

                if(referenceClass.Methods.Any(m => m.Name == calledMethod.Name))
                {
                    result.Add(
                        $"{indentation}this.{calledMethod.Name}() -> {referenceClass.Name}.{calledMethod.Name}()");
                }
            }
        }

        foreach(var calledMethod in methodReference.Methods)
        {
            var slead = true;
            foreach(var localVar in methodReference.LocalVariables)
            {
                var classType = localVar.LocalVariableType as Class;
                var c = classType.Methods.Any(m => m.Name == calledMethod.Name);
                var aux = classType;
                var i = 0;
                if(classType != null && c)
                {
                    var classInherited = classType.HierarchyClass as Class;
                    var cant = classInherited.Methods.Count;
                    while(aux != null && i < cant)
                    {
                        if(classInherited != null && classInherited.Methods[i].Stereotype != KeyWord.Sealed)
                        {
                            var indentation = new string('\t', _indentacion);
                            result.Add(
                                $"{indentation}{localVar.Name}.{classInherited.Methods[i].Name}() -> {aux.Name}.{classInherited.Methods[i].Name}()");
                        }

                        aux = (Class)aux.HierarchyClass;
                        i++;
                    }

                    while(classInherited != null)
                    {
                        Sobreescrito(classType, result, classInherited);
                        classType = classInherited;
                        classInherited = (Class)classInherited.HierarchyClass;
                    }

                    MethodTThis(classType, result);
                }
            }
        }

        return string.Join("\n", result);
    }

    private static void MethodTThis(Class classType, List<string> result)
    {
        var indentation = new string('\t', _indentacion + 1);
        if(classType.Stereotype == null)
        {
            foreach(var m in classType.Methods)
            {
                if(!(m.Methods.Count > 0))
                {
                    result.Add($"{indentation}this.{m.Name}() -> {classType.Name}.{m.Name}()");
                    _indentacion++;
                }
            }
        }
    }

    private static void FirstLine(Class reference, Class objectToCreate, Method methodReference, bool flag,
        List<string> result)
    {
        var indentation = new string('\t', _indentacion);
        if(!flag)
        {
            result.Add(
                $"{indentation}{reference.Name}.{methodReference.Name}() -> {objectToCreate.Name}.{methodReference.Name}()");
        }
        else
        {
            result.Add(
                $"{indentation}{reference.Name}.{methodReference.Name}() -> {reference.Name}.{methodReference.Name}()");
        }

        _indentacion++;
    }

    private void Sobreescrito(Class classType, List<string> result, Class parent)
    {
        foreach(var methodInParent in classType.Methods)
        {
            var cant = ContarOcurrenciasMetodoEnJerarquia(methodInParent);
            var indentation = new string('\t', _indentacion + 1);
            if(cant > 1)
            {
                result.Add($"{indentation}base.{methodInParent.Name}() -> {parent.Name}.{methodInParent.Name}()");
                _indentacion++;
            }
        }
    }

    public int ContarOcurrenciasMetodoEnJerarquia(Method metodoNombre)
    {
        var count = 0;
        var met = metodoNombre;

        foreach(var m in met.Methods)
        {
            if(m.Name == metodoNombre.Name)
            {
                count++;
            }

            if(m.Methods.Count > 0)
            {
                count += ContarOcurrenciasMetodoEnJerarquia(m);
            }
        }

        return count;
    }

    public override bool Equals(object? obj)
    {
        return obj is Method other && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
