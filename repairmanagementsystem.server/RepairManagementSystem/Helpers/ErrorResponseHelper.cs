using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RepairManagementSystem.Helpers
{
    public static class ErrorResponseHelper
    {
        public static ObjectResult CreateProblemDetails(HttpContext httpContext, string type, string title, int status, object errors)
        {
            var problemDetails = new ProblemDetails
            {
                Type = type,
                Title = title,
                Status = status,
                Detail = null,
                Instance = httpContext.TraceIdentifier
            };
            problemDetails.Extensions["errors"] = errors;
            return new ObjectResult(problemDetails) { StatusCode = status };
        }
    }
}
