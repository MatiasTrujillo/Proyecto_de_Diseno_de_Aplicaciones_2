using Simulator.DTO;

namespace Simulator.IBussinesLogic;

public interface ISessionService
{
    Guid SignIn(SessionDTO sessionDto);
    void LogIn(Guid token);
    void LogOut(Guid token);
    bool IsActive(Guid token);
}
