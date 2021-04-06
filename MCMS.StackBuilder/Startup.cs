using System;
using MCMS.Base.Helpers;
using MCMS.Base.SwaggerFormly.Models;
using MCMS.Builder;
using MCMS.Common;
using MCMS.StackBuilder.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MCMS.StackBuilder
{
    public class Startup
    {
        private readonly MApp _mApp;

        public Startup(IWebHostEnvironment environment)
        {
            _mApp = new MAppBuilder(environment)
                .AddSpecifications<MCommonSpecifications>(configure: spec =>
                {
                    spec.Config.IncludeClipboardJs = true;
                    spec.Config.IncludeHighlightJs = true;
                })
                .AddSpecifications<StackBuilderSpecifications>()
                .WithPostgres<ApplicationDbContext>()
                .WithSwagger(new SwaggerConfigOptions
                    {
                        Title = "MCMS Stack Builder",
                        Version = "v1"
                    },
                    new SwaggerConfigOptions
                    {
                        Title = "MCMS Stack Builder API",
                        Version = "v1",
                        UiType = DocsUiType.Both
                    })
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _mApp.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, ILogger<Startup> logger)
        {
            _mApp.Configure(app, serviceProvider);
            if (!string.IsNullOrEmpty(Env.Get("ASPNETCORE_URLS")) && !_mApp.HostEnvironment.IsProduction())
            {
                logger.LogInformation("Open your browser at {Url}",
                    Env.Get("ASPNETCORE_URLS").Replace("0.0.0.0", "localhost"));
                logger.LogInformation("Swagger url {Url}{Path}",
                    Env.Get("ASPNETCORE_URLS").Replace("0.0.0.0", "localhost"),
                    Utils.UrlCombine(RoutePrefixes.RoutePrefix, "api/docs"));
            }
        }
    }
}