using GestionDeTareas.Core.Application.DTos.Account;
using GestionDeTareas.Core.Application.DTos.Account.Authenticate;
using GestionDeTareas.Core.Application.DTos.Account.Register;
using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Utils;

namespace GestionDeTareas.Core.Application.Interfaces.Service
{
    public interface IAccountService
    {
        Task<ApiResponse<RegisterResponse>> RegisterAccountAsync(RegisterRequest request,string roles);

        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);

        Task<IEnumerable<AccountDTos>> GetAllAsync();
    }
}
