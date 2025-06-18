using Simulator.Domain;

namespace Simulator.IBussinesLogic;

public interface ISessionService
{
    Guid SignIn(Session sessionDto);
    void LogIn(Guid token);
    void LogOut(Guid token);
    bool IsActive(Guid token);
}
