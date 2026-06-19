using Microsoft.AspNetCore.Mvc;
using NextAuto.Application.Interfaces.IServiceResult;

namespace NextAuto.WebApi.Common;

public static class ServiceResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this IServiceResult<T> result)
    {
        if (result.IsSuccess)
        {
            return new ObjectResult(result.Value) { StatusCode = result.StatusCode };
        }

        return new ObjectResult(new { error = result.ErrorMessage }) { StatusCode = result.StatusCode };
    }
}
