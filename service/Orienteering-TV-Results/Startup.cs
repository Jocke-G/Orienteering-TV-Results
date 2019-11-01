using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using OrienteeringTvResults.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace OrienteeringTvResults
{
    internal class Startup
    {
        private readonly string DefaultCorsPolicy = "defaultCorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            services.Configure<ApplicationConfiguration>(configuration);

            services.AddSingleton<MqttHostedService>();
            services.AddSingleton<ResultsAdapter>();

            var applicationConfiguration = configuration.Get<ApplicationConfiguration>();
            if (applicationConfiguration.EnablePollService)
            {
                services.AddSingleton<IHostedService, PollHostedService>();
            }

            services
                .AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(o => o.AddPolicy(DefaultCorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Orienteering TV Results", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(DefaultCorsPolicy);

            app.UseMvc();
        }
    }
}
