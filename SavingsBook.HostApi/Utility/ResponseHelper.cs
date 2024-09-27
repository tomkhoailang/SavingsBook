using Microsoft.AspNetCore.Mvc;
using SavingsBook.Application.Contracts.Common;

namespace SavingsBook.HostApi.Utility;

public static class ResponseHelper
{
    public static IActionResult FormatResponse<T>(ResponseDto<T> response)
    {
        return response.StatusCode switch
        {
            200 => // OK
                response.Data != null ? new OkObjectResult(response.Data) : new OkResult(),
            201 => // Created
                response.Data != null ? new CreatedResult("", response.Data) : new CreatedResult("", null),
            204 => // No Content
                new NoContentResult(),
            400 => // Bad Request
                new BadRequestObjectResult(response.Message),
            404 => // Not Found
                new NotFoundObjectResult(response.Message),
            500 => // Internal Server Error
                new ObjectResult(response.Message) { StatusCode = 500 },
            _ => new ObjectResult(response.Message) { StatusCode = response.StatusCode }
        };
    }
}