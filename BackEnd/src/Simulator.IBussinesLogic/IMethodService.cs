using Simulator.Domain;

namespace Simulator.IBussinesLogic;

public interface IMethodService
{
    Guid CreateMethod(Method method);
    void AssociateMethodWithClass(Guid methodId, Guid classId);
    void DeleteMethod(Guid id);
    Guid AssociateInvokeMethod(InvokedMethod invokedMethod);
    void AddAMethod(Guid id, Guid invokedMethodId);
    void AddLocalVariableToMethod(Guid methodId, Guid localVariableId);
    void AddParameterToMethod(Guid methodId, Guid parameterId);
    string ExecuteMethod(Guid referenceId, Guid objectToCreateId, Guid methodId, List<TypeDTO> parameterTypes);
    Guid CreateMethodSignature(MethodSignature method);
    List<Method> GetMethods();
    List<InvokedMethod> GetInvokedMethodById();
    List<MethodSignature> GetMethodSignatures();
}
