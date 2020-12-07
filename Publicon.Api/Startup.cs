using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publicon.Api.Authorization;
using Publicon.Api.Extensions;
using Publicon.Api.HostedServices;
using Publicon.Api.Middleware;
using Publicon.API.Binders.BodyandRoute;
using Publicon.Core.DAL;
using Publicon.Core.Modules;
using Publicon.Infrastructure.Modules;
using Publicon.Infrastructure.Settings;
using System;

namespace Publicon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PubliconContext>(options =>
            {
                options.UseLazyLoadingProxies();
                //options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
                options.UseSqlServer(Configuration.GetConnectionString("SqlExpress"));
            });

            services.AddSingleton(x => new BlobServiceClient(Environment.GetEnvironmentVariable("ConnectionStrings__AzureStorage")));

            services.AddControllers(opt =>
            {
                opt.ModelBinderProviders.InsertBodyAndRouteBinding();
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddOptions();

            var mailSettingsSection = Configuration.GetSection(nameof(MailSettings));
            services.Configure<MailSettings>(mailSettingsSection);

            var frontendSettingsSection = Configuration.GetSection(nameof(FrontendSettings));
            services.Configure<FrontendSettings>(frontendSettingsSection);

            services.AddHttpContextAccessor();
            services.AddScoped<IAuthorizationHandler, MustBeOwnerOrAdminHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeOwnerOrAdmin", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.AddRequirements(
                        new MustBeOwnerOrAdminRequirement());
                });
            });

            services.AddHostedService<SendNotificationService>();
            services.AddAuthenticationConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddRepositoriesModule();
            services.AddManagersModule();
            services.AddMediatRModule();
            services.AddMapperModule();
            services.AddPasswordHasherModule();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMiddleware(typeof(GlobalExceptionMiddleware));
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware(typeof(GlobalExceptionMiddleware));
            }

            //app.UseHttpsRedirection();
            app.UseCorsPolicy();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publicon v1");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
