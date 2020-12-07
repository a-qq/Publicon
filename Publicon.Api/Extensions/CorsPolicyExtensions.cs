using Microsoft.AspNetCore.Builder;

namespace Publicon.Api.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(opts =>
            {
                opts.AllowAnyOrigin();
                opts.AllowAnyMethod();
                opts.AllowAnyHeader();
            });
        }
    }
}

