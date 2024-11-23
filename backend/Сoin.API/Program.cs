using �oin.Data;
using �oin.DTO;
using �oin.Contracts.Repo;
using �oin.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using �oin.Data.Repository;
using �oin.Data.Persistence;
using �oin.Contracts.Persistence;
using Base.DTO;
using Base.Data;
using �oin.Logic;

namespace �oin.Api
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
            Configure(host, builder.Environment);
            await SeedDatabaseAsync(host);
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
            services.AddScoped(typeof(ICoinRepository), typeof(CoinRepository));
            services.AddScoped(typeof(ICoinExchangeRepository), typeof(CoinExchangeRepository));
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped(typeof(ICoinAPI), typeof(CoinAPI));
            services.AddAutoMapper(typeof(Mapper));

            // add services CORS
            services.AddCors();

            string connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection)); //SQL
            //services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(connection)); //Memory
            services.AddMemoryCache();  //System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MailGraphAnalysis.Contracts.Services.ILetterService Lifetime: Scoped ImplementationType: 
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // add services CORS
            app.UseCors(builder => builder
                .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();
            app.UseEndpoints(x => x.MapDefaultControllerRoute());

        }

        private static async Task SeedDatabaseAsync(WebApplication host)
        {

            using (var scope = host.Services.CreateScope())
            {
                await DataSample.InitializeAsync(scope);
            }
        }

    }
}