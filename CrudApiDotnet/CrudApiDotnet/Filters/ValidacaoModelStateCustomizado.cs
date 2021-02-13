﻿using CrudApiDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CrudApiDotnet.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validaCampoViewModelOutput = new ValidaCampoViewModelOutput(context.ModelState
                    .SelectMany(sm => sm.Value.Errors)
                    .Select(s => s.ErrorMessage));
                context.Result = new BadRequestObjectResult(validaCampoViewModelOutput);                
            }
        }
    }
}
