using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RepairManagementSystem.Helpers
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public string Message { get; private set; } = string.Empty;
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }

        public ObjectResult? ProblemDetailsResult { get; set; }

        private Result() { }

        public static Result<T> Ok(T data)
        {
            return new Result<T>
            {
                Data = data,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public static Result<T> Fail(int statusCode, string message)
        {
            int mappedStatusCode = statusCode switch
            {
                400 => StatusCodes.Status400BadRequest,
                401 => StatusCodes.Status401Unauthorized,
                403 => StatusCodes.Status403Forbidden,
                404 => StatusCodes.Status404NotFound,
                409 => StatusCodes.Status409Conflict,
                500 => StatusCodes.Status500InternalServerError,
                _ => statusCode
            };

            var typeName = typeof(T).Name;
            var errorKey = char.ToLowerInvariant(typeName[0]) + typeName[1..];
            var errors = new Dictionary<string, object?>
            {
                [errorKey] = message
            };

            var problemDetails = new ProblemDetails
            {
                Status = mappedStatusCode,
                Title = message,
                Type = Result.GetLinkByStatusCode(mappedStatusCode),
                Detail = string.Empty
            };

            problemDetails.Extensions["message"] = message;

            var result = new Result<T>
            {
                Data = default,
                Message = message,
                IsSuccess = false,
                StatusCode = mappedStatusCode,
                ProblemDetailsResult = new ObjectResult(problemDetails) { StatusCode = mappedStatusCode }
            };
            return result;
        }

        public static Result<T> Fail(string message = "An error occurred.")
        {
            return Fail(StatusCodes.Status400BadRequest, message);
        }
    }

    public class Result
    {
        public string Message { get; private set; } = string.Empty;
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }
        public ObjectResult? ProblemDetailsResult { get; set; }
        private Result() { }
        public static Result Ok(string message = "Success")
        {
            return new Result
            {
                Message = message,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK
            };
        }
        public static Result Fail(int statusCode, string message)
        {
            int mappedStatusCode = statusCode switch
            {
                400 => StatusCodes.Status400BadRequest,
                401 => StatusCodes.Status401Unauthorized,
                403 => StatusCodes.Status403Forbidden,
                404 => StatusCodes.Status404NotFound,
                409 => StatusCodes.Status409Conflict,
                500 => StatusCodes.Status500InternalServerError,
                _ => statusCode
            };
            var errors = new Dictionary<string, object?>
            {
                ["error"] = message
            };
            var problemDetails = new ProblemDetails
            {
                Status = mappedStatusCode,
                Title = message,
                Type = GetLinkByStatusCode(mappedStatusCode),
                Detail = string.Empty
            };

            problemDetails.Extensions["message"] = message;
            return new Result
            {
                Message = message,
                IsSuccess = false,
                StatusCode = mappedStatusCode,
                ProblemDetailsResult = new ObjectResult(problemDetails) { StatusCode = mappedStatusCode }
            };
        }
        public static Result Fail(string message = "An error occurred.")
        {
            return Fail(StatusCodes.Status400BadRequest, message);
        }
        public static string GetLinkByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                StatusCodes.Status401Unauthorized => "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                StatusCodes.Status403Forbidden => "https://tools.ietf.org/html/rfc9110#section-15.5.3",
                StatusCodes.Status404NotFound => "https://tools.ietf.org/html/rfc9110#section-15.5.4",
                StatusCodes.Status409Conflict => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                _ => "https://datatracker.ietf.org/doc/html/rfc9110"
            };
        }
    }
}
