namespace GestionDeTareas.Core.Application.DTos.Account;

public sealed record AccountDTos
(
    string? UserId,
    string? FirstName,
    string? LastName,
    string? Email, 
    string? Username,
    string? PhoneNumber
);