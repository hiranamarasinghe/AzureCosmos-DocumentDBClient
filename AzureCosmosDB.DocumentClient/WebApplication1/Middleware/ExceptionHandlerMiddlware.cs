using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClientDemo.WebApi.Middleware
{
    public class ExceptionHandlerMiddlware
    {
        private RequestDelegate _next;
        public ExceptionHandlerMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await context.Response.WriteAsync(e.ToString());
            }
        }
    }
}
