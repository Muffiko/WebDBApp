using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Helpers;
namespace RepairManagementSystem.Extensions
{
    public static class ControllerResultExtensions
    {
        public static IActionResult ToApiResponse<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess && result.Data != null)
            {
                return controller.Ok(result.Data);
            }
            if (result.IsSuccess)
            {
                return controller.Ok(new { message = result.Message });
            }
            if (result.ProblemDetailsResult != null)
            {
                if (result.ProblemDetailsResult.Value is ProblemDetails pd && string.IsNullOrEmpty(pd.Instance))
                {
                    pd.Instance = controller.HttpContext.TraceIdentifier;
                }
                return result.ProblemDetailsResult;
            }
            return controller.StatusCode(result.StatusCode, new { error = result.Message });
        }
        public static IActionResult ToApiResponse(this ControllerBase controller, Result result)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(new { message = result.Message });
            }
            if (result.ProblemDetailsResult != null)
            {
                if (result.ProblemDetailsResult.Value is ProblemDetails pd && string.IsNullOrEmpty(pd.Instance))
                {
                    pd.Instance = controller.HttpContext.TraceIdentifier;
                }
                return result.ProblemDetailsResult;
            }
            return controller.StatusCode(result.StatusCode, new { error = result.Message });
        }
    }
}