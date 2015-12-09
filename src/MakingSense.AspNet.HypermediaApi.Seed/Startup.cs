using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.AspNet.Documentation;
using MakingSense.AspNet.HypermediaApi.Formatters;
using MakingSense.AspNet.HypermediaApi.ValidationFilters;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace MakingSense.AspNet.HypermediaApi.Seed
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new HypermediaApiJsonOutputFormatter());

                options.InputFormatters.Clear();
                options.InputFormatters.Add(new HypermediaApiJsonInputFormatter());

                options.Filters.Add(new PayloadValidationFilter());
                options.Filters.Add(new RequiredPayloadFilter());
            });

            services.AddTransient<System.IdentityModel.Tokens.ISecurityTokenValidator, SeedTokenValidator>();

            services.AddLinkHelper<SeedLinkHelper>();
        }

        public void Configure(IApplicationBuilder app, IApplicationEnvironment appEnv)
        {
            app.UseApiErrorHandler();

            app.UseSimpleTokenAuthentication(o =>
            {
                o.AutomaticAuthentication = true;
            });

            app.UseMvc();

            app.UseStaticFiles();

            UseDocumentation(app, appEnv);

            app.UseNotFoundHandler();
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
