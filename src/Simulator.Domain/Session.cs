using System.ComponentModel.DataAnnotations;

namespace Simulator.Domain;

public class Session
{
    [Key]
    public Guid Token { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = false;

    public Session() { }

    public Session(Guid token)
    {
        Token = token;
    }
}
