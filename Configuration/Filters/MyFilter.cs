using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace restAPI.Configuration.Filters;

public class MyFilter: IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"This filter is executed on: OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"This filter is executed on: OnActionExecuted");
    }
}


