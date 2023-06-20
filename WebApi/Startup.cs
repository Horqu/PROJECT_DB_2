using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // dodajemy nasz serwis StudentService do kontenera DI
            //services.AddSingleton(new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<StudentService>();
            //services.AddTransient<KursService>(provider => new KursService(provider.GetRequiredService<SqlConnection>()));

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IStudentService>(provider => new StudentService(connectionString));

            // dodajemy serwis KursService do kontenera DI
            services.AddTransient<IKursService>(provider => new KursService(connectionString));
            services.AddTransient<INauczycielService>(provider => new NauczycielService(connectionString));
            services.AddTransient<IKursRozkladService>(provider => new KursRozkladService(connectionString));
            services.AddTransient<ILiczbaOcenService>(provider => new LiczbaOcenService(connectionString));
            services.AddTransient<ILiczbaOcenPozNegService>(provider => new LiczbaOcenPozNegService(connectionString));
            //services.AddTransient<OcenaService>(provider => new OcenaService(connectionString));
            services.AddTransient<IOcenaService>(provider => new OcenaService(connectionString));

            //services.AddScoped<IOcenaService, OcenaService>();
            services.AddTransient<DatabaseService>(provider => new DatabaseService(connectionString));
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
