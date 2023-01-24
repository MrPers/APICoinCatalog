using MailGraphAnalysis.DB;
using MailGraphAnalysis.DTO;
using MailGraphAnalysis.Services;
using MailGraphAnalysis.Contracts.Business;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.Repository;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace MailGraphAnalysis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigurationManager configuration = builder.Configuration;
            IServiceCollection services = builder.Services;
            ConfigureServices(services, configuration);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            var host = builder.Build();
            var env = builder.Environment;
            SeedDatabaseAsync(host);
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
            services.Configure<MySettingsModelDto>(configuration.GetSection("MailConnection"));

            services.AddScoped(typeof(ILetterRepository), typeof(LetterRepository));
            services.AddScoped(typeof(ICoinRepository), typeof(CoinRepository));
            services.AddScoped(typeof(ICoinExchangeRepository), typeof(CoinExchangeRepository));
            services.AddScoped<ILetterService, LetterService>();
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped(typeof(ILetterBusiness), typeof(LetterBusines));
            services.AddAutoMapper(typeof(Mapper));

            // add services CORS
            services.AddCors();

            string connection = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(connection)); //SQL
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(connection)); //Memory
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

        private static async void SeedDatabaseAsync(WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                await DataSample.InitializeAsync(scope);
            }
        }

    }
}