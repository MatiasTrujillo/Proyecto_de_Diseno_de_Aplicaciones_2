using System;
using Simulator.Domain;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class SessionService(ISessionRepository sessionRepository) : ISessionService
{
    public Guid SignIn(Session session)
    {
        var existingSession = sessionRepository.Find(session.Token);
        if(existingSession != null)
        {
            throw new InvalidOperationException("A session with this token already exists.");
        }

        sessionRepository.Add(session);
        return session.Token;
    }

    public void LogIn(Guid token)
    {
        var session = sessionRepository.Find(token);
        ValidateSession(session);

        if(session!.IsActive)
        {
            throw new InvalidOperationException("Session is already active.");
        }

        session.IsActive = true;
        sessionRepository.Update(session);
    }

    public void LogOut(Guid token)
    {
        var session = sessionRepository.Find(token);
        ValidateSession(session);

        if(!session!.IsActive)
        {
            throw new InvalidOperationException("Session is already inactive.");
        }

        session.IsActive = false;
        sessionRepository.Update(session);
    }

    public bool IsActive(Guid token)
    {
        var session = sessionRepository.Find(token);
        return session.IsActive;
    }

    private void ValidateSession(Session? session)
    {
        if(session == null)
        {
            throw new UnauthorizedAccessException("Invalid or expired session.");
        }
    }
}
