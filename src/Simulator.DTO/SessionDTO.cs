using Simulator.Domain;
using Type = System.Type;

namespace Simulator.DTO;

public class SessionDTO
{
    public Guid Token { get; set; } = Guid.Empty;
    public Session ToEntity()
    {
        return new Session(Token);
    }
}
