using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    Guid CreateMethod(MethodDTO method);
    void AssociateMethodWithClass(Guid methodId, Guid classId);
    void DeleteMethod(Guid id);
    Guid AssociateInvokeMethod(InvokedMethodDTO invokedMethod);
    void AddAMethod(Guid id, Guid invokedMethodId);
    void AddLocalVariableToMethod(Guid methodId, Guid localVariableId);
    void AddParameterToMethod(Guid methodId, Guid parameterId);
    string ExecuteMethod(Guid referenceId, Guid objectToCreateId, Guid methodId, List<TypeDTO> parameterTypes);
    Guid CreateMethodSignature(MethodSignatureDTO method);
    List<MethodDTO> GetMethods();
    List<InvokedMethodDTO> GetInvokedMethodById();
    List<MethodSignatureDTO> GetMethodSignatures();
}
