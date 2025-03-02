namespace GestionDeTareas.Core.Application.DTos.Account.Register
{
    public sealed record RegisterResponse
    (
        string? UserId,
        string? Username,
        string? Email
    );
}
