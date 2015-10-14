using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.FileProviders;
using Microsoft.Dnx.Runtime;
using MakingSense.AspNet.Documentation;

namespace MakingSense.AspNet.HypermediaApi.Seed
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IApplicationEnvironment appEnv)
        {
            app.UseStaticFiles();

            UseDocumentation(app, appEnv);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private static void UseDocumentation(IApplicationBuilder app, IApplicationEnvironment appEnv)
        {
            var documentationFilesProvider = new PhysicalFileProvider(appEnv.ApplicationBasePath);
            app.UseDocumentation(new DocumentationOptions()
            {
                DefaultFileName = "index",
                RequestPath = "/docs",
                NotFoundHtmlFile = documentationFilesProvider.GetFileInfo("DocumentationTemplates\\NotFound.html"),
                LayoutFile = documentationFilesProvider.GetFileInfo("DocumentationTemplates\\Layout.html")
            });
        }
    }
}
