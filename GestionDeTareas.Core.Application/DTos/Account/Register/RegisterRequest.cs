namespace GestionDeTareas.Core.Application.DTos.Account.Register
{
    public sealed record RegisterRequest
    (
        string? Username,
        string? FirstName,
        string? LastName,
        string? Email,
        string PhoneNumber,
        string? Password
    );
}
