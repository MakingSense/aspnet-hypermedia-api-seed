using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNetCore.Documentation;
using MakingSense.AspNetCore.HypermediaApi.ValidationFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MakingSense.AspNetCore.HypermediaApi.Seed
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc(options =>
            {
                options.Filters.Add(new PayloadValidationFilter());
                options.Filters.Add(new RequiredPayloadFilter());
            })
            .SetHypermediaApiFormatters();

            services.AddTransient<Microsoft.IdentityModel.Tokens.ISecurityTokenValidator, SeedTokenValidator>();

            services.AddLinkHelper<SeedLinkHelper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            app.UseHypermediaApiErrorHandler();

            app.UseSimpleTokenAuthentication(new SimpleTokenAuthenticationOptions()
            {
                AutomaticAuthenticate = true
            });

            app.UseMvc();

            app.UseStaticFiles();

            UseDocumentation(app, hostingEnvironment);

            app.UseNotFoundHandler();
        }

        private static void UseDocumentation(IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            var documentationFilesProvider = hostingEnvironment.ContentRootFileProvider;
            app.UseDocumentation(new DocumentationOptions()
            {
                DefaultFileName = "index",
                RequestPath = "/docs",
                NotFoundHtmlFile = documentationFilesProvider.GetFileInfo("DocumentationTemplates\\NotFound.html"),
                LayoutFile = documentationFilesProvider.GetFileInfo("DocumentationTemplates\\Layout.html")
            });
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    }
}
