using GestionDeTareas.Core.Application.DTos.Account.Authenticate;
using GestionDeTareas.Core.Application.DTos.Account.Register;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeTareas.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountContoller : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountContoller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudentAsync([FromBody] RegisterRequest registerRequest)
        {
            var result = await _accountService.RegisterAccountAsync(registerRequest,Roles.Student.ToString().ToLower());
            if(!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterRequest registerRequest)
        {
            var result = await _accountService.RegisterAccountAsync(registerRequest, Roles.Admin.ToString().ToLower());
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest authenticate)
        {
            var request = await _accountService.AuthenticateAsync(authenticate);
            return request.StatusCodes switch
            {
                404 => NotFound(ApiResponse<string>.ErrorResponse($" Email {request.Email} not found")),
                400 => BadRequest(ApiResponse<string>.ErrorResponse($"Account not confirmed for {request.Email}")),
                401 => Unauthorized(ApiResponse<string>.ErrorResponse($"Invalid credentials for {request.Email}")),
                _ => Ok(ApiResponse<AuthenticateResponse>.SuccessResponse(request))
            };
        }
    }
}
