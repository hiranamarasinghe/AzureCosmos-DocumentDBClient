using DocumentClient.Domain;
using DocumentClient.Domain.Contracts.Shared;
using DocumentClientDemo.Domain.Contracts.Business;
using DocumentClientDemo.Services.Business;
using DocumentClientDemo.Services.Shared;
using DocumentClientDemo.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApplication1
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
            var dbConfigSettingConfig = Configuration.GetSection("DBSettings");
            DBSetting dbSetting = new DBSetting();
            dbConfigSettingConfig.Bind(dbSetting);
            services.AddSingleton(s => dbSetting);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDocumentDBService, DocumentDBService>();
            services.AddMvc();
            services.AddSwaggerGen(swagger =>
            {
                swagger.DescribeAllEnumsAsStrings();
                swagger.DescribeAllParametersInCamelCase();
                swagger.SwaggerDoc("v1", new Info { Title = "Cosmos DB Document client", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMiddleware<ExceptionHandlerMiddlware>();

            app.UseMvc();
        }
    }
}
