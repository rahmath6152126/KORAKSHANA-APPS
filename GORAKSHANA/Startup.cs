using System.Net;
using GORAKSHANA.IService;
using GORAKSHANA.Models;
using GORAKSHANA.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GORAKSHANA
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

            services.Configure<IISOptions>(x => x.AutomaticAuthentication = false);
            // requires using Microsoft.Extensions.Options
            services
                .Configure<DatabaseSettings>(Configuration
                    .GetSection(nameof(DatabaseSettings)));

            services
                .AddSingleton<IDatabaseSettings>(sp =>
                    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);


            services.AddTransient<ISponserServices, SponserServices>();
            services.AddTransient<IMasterService, MasterService>();
            services
                .AddCors(x =>
                {
                    x
                        .AddPolicy("*",
                        builder =>
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
                });

            services.AddSwaggerDocument();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app
                .UseExceptionHandler(options =>
                {
                    options
                        .Run(async context =>
                        {
                            context.Response.StatusCode =
                                (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var ex =
                                context
                                    .Features
                                    .Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                                await context
                                    .Response
                                    .WriteAsync(err)
                                    .ConfigureAwait(false);
                            }
                        });
                });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app
                .UseCors(Options =>
                    Options
                        .SetIsOriginAllowed(x => _ = true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
