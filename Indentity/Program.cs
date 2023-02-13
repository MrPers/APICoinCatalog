using Duende.IdentityServer.Services;
using Indentity.API;

namespace Indentity.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigurationManager configuration = builder.Configuration;
            IServiceCollection services = builder.Services;
            ConfigureServices(services, configuration);

            var host = builder.Build();
            var env = builder.Environment;
            Configure(host, env);
            await host.RunAsync();

        }

        public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddHttpClient(); // to send requests

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //register a class

            services.AddIdentityServer()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddDeveloperSigningCredential();

            services.AddSingleton<ICorsPolicyService>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();     //CORE от IS4
                return new DefaultCorsPolicyService(logger)
                {
                    AllowedOrigins = { "http://localhost:4200" }
                };
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseIdentityServer();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x => x.MapDefaultControllerRoute());
        }
    }
}

