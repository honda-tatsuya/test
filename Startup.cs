using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace ConsoleApplication
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app) {
            app.UseOwin(pipeline => pipeline.UseNancy());
        }
    }
}