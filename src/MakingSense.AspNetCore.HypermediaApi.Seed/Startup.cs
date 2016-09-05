using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNetCore.Documentation;
using MakingSense.AspNetCore.HypermediaApi.Formatters;
using MakingSense.AspNetCore.HypermediaApi.ValidationFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
                //TODO: fix it
                //options.OutputFormatters.Clear();
                //options.OutputFormatters.Add(new HypermediaApiJsonOutputFormatter());

                //options.InputFormatters.Clear();
                //options.InputFormatters.Add(new HypermediaApiJsonInputFormatter());

                options.Filters.Add(new PayloadValidationFilter());
                options.Filters.Add(new RequiredPayloadFilter());
            });

            //services.AddTransient<Microsoft.IdentityModel.Tokens.ISecurityTokenValidator, SeedTokenValidator>();

            services.AddLinkHelper<SeedLinkHelper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            app.UseApiErrorHandler();

            //app.UseSimpleTokenAuthentication(o =>
            //{
            //    o.AutomaticAuthenticate = true;
            //    o.AutomaticChallenge = true;
            //});

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
