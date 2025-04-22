namespace Simulator.Domain;

public class Method
{
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
        var result = new List<string>();
        var flag = false;

        if(objectToCreate.HierarchyClass != null)
        {
            flag = reference.Id != objectToCreate.HierarchyClass.Id;
        }

        if(!flag)
        {
            result.Add($"{reference.Name}.{methodReference.Name}() -> {objectToCreate.Name}.{methodReference.Name}()");
        }
        else
        {
            result.Add($"{reference.Name}.{methodReference.Name}() -> {reference.Name}.{methodReference.Name}()");
        }

        foreach(var calledMethod in methodReference.Methods)
        {
            var found = false;
            foreach(var param in methodReference.Parameters)
            {
                if(param.ParameterType is Class paramClass)
                {
                    if(paramClass.Methods.Any(m => m.Name == calledMethod.Name))
                    {
                        result.Add($"{param.Name}.{calledMethod.Name}() -> {paramClass.Name}.{calledMethod.Name}()");
                        found = true;
                        break;
                    }
                }
            }

            if(reference is Class referenceClass)
            {
                foreach(var attr in referenceClass.Attributes)
                {
                    if(attr.Type is Class attrClass)
                    {
                        if(attrClass.Methods.Any(m => m.Name == calledMethod.Name))
                        {
                            result.Add($"{attr.Name}.{calledMethod.Name}() -> {attrClass.Name}.{calledMethod.Name}()");
                            found = true;
                            break;
                        }

                        var parent = attrClass.HierarchyClass;
                        while(parent != null)
                        {
                            var methodInParent = parent.Methods.FirstOrDefault(m => m.Name == calledMethod.Name);
                            if(methodInParent != null)
                            {
                                result.Add(
                                    $"{attr.Name}.{calledMethod.Name}() -> {parent.Name}.{methodInParent.Name}()");
                                found = true;
                                break;
                            }

                            parent = parent.HierarchyClass;
                        }
                    }
                }

                if(found)
                {
                    continue;
                }

                if(referenceClass.Methods.Any(m => m.Name == calledMethod.Name))
                {
                    result.Add($"this.{calledMethod.Name}() -> {referenceClass.Name}.{calledMethod.Name}()");
                    continue;
                }
            }

            result.Add($"this.{calledMethod.Name}() -> {reference.Name}.{calledMethod.Name}()");
        }

        return string.Join("\n", result);
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
