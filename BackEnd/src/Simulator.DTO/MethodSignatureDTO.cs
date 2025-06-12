using Simulator.Domain;

namespace Simulator.DTO;

public class MethodSignatureDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public TypeDTO? ReturnType { get; set; } = null!;

    public MethodSignature ToEntity()
    {
        var returnType = ReturnType?.ToEntity() ?? null;
        return new MethodSignature(Name, returnType, []);
    }
}
