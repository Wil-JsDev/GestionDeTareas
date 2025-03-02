
namespace GestionDeTareas.Core.Domain.Utils
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public string? ErrorMessage { get; set; }

        public static ApiResponse<T> SuccessResponse(T data) => 
            new ApiResponse<T> { IsSuccess = true ,Data = data };

        public static ApiResponse<T> ErrorResponse (string errorMessage) => 
            new ApiResponse<T> { IsSuccess = false ,ErrorMessage = errorMessage };
    }
}
