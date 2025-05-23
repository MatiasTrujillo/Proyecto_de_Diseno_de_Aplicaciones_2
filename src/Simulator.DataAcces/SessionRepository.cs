using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.DataAcces;

public class SessionRepository(SqlContext context) : ISessionRepository
{
    public void Add(Session token)
    {
        context.Sessions.Add(token);
        context.SaveChanges();
    }

    public void Delete(Guid token)
    {
        var session = context.Sessions.Find(token);
        if(session != null)
        {
            context.Sessions.Remove(session);
            context.SaveChanges();
        }
    }

    public void Update(Session token)
    {
        var existingSession = Find(token.Token);
        if(existingSession != null)
        {
            context.Entry(existingSession).CurrentValues.SetValues(token);
            context.SaveChanges();
        }
    }

    public Session Find(Guid token)
    {
        return context.Sessions.Find(token);
    }

    public List<Session> FindAll()
    {
        return context.Sessions.ToList();
    }
}
