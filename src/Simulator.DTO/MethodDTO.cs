using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class MethodDTO
{
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }
    public KeyWord? Stereotype { get; set; }
    private List<LocalVariableDTO> LocalVariable { get; set; } = [];
    private List<ParameterDTO> Parameters { get; set; } = [];
    private List<MethodDTO> Methods { get; set; } = [];
    public TypeDTO ReturnType { get; set; } = new TypeDTO();

    public Method ToEntity()
    {
        Type returnType = ReturnType.ToEntity();
        List<LocalVariable> localVariable = LocalVariable.Select(lv => lv.ToEntity()).ToList();
        List<Parameter> parameters = Parameters.Select(p => p.ToEntity()).ToList();
        List<Method> methods = Methods.Select(m => m.ToEntity()).ToList();
        return new Method(Name, Accessibility, Stereotype, methods, parameters, localVariable, returnType);
    }
}
