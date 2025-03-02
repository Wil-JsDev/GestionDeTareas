
namespace GestionDeTareas.Core.Application.DTos.Account.Authenticate
{
    public sealed record AuthenticateRequest
    (
        string? Email,
        string? Password
    );
}
