using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using TesteTailorIT.Api.Response;
using TesteTailorIT.Domain.Config;
using TesteTailorIT.Infra.CrossCutting.AutoMapper;
using TesteTailorIT.Infra.CrossCutting.Ioc;
using TesteTailorIT.Infra.Data.DataSample;
using TesteTailorIT.Infra.Data.Migrations;
using TesteTailorIT.Infra.Data.Repositories;

namespace TesteTailorIT.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    var resolver = opt.SerializerSettings.ContractResolver;
                    if (resolver != null)
                    {
                        (resolver as DefaultContractResolver).NamingStrategy = null;
                    }
                });

            services.AddDbContext<FuncionarioRepository>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TesteTailorITConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Teste TailorIT", Version = "v1" });
            });

            services.AddCors();

            services.AddAutoMapper(config =>
            {
                config.ForAllMaps((map, expression) =>
                {
                    foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
                        expression.ForMember(unmappedPropertyName,
                            configurationExpression => configurationExpression.Ignore());
                });

                config.AddProfiles(typeof(ApplicationProfile).Assembly);
            });

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("TesteTailorITConnection"))
                    .ScanIn(typeof(BaseLine).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            ServiceProvider = new AutofacServiceProvider(ConfigureAutoFac(services));

            return ServiceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMigrationRunner migrationRunner)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Younder");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseMvc();

            migrationRunner.MigrateUp();

            var context = ServiceProvider.GetRequiredService<FuncionarioRepository>();
            DataSample.Initialize(context);
        }

        private static Autofac.IContainer ConfigureAutoFac(IServiceCollection services)
        {
            var configbuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", true, true)
             .AddEnvironmentVariables();

            var config = configbuilder.Build();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();
            services.Configure<Configuracoes>(config);
            services.AddScoped(typeof(IPresenter), typeof(Presenter));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var configurationModule = new ConfigurationModule(configbuilder.Build());

            builder.RegisterModule(configurationModule);

            builder.RegisterModule(new ApplicationModule());

            return builder.Build();
        }
    }
}