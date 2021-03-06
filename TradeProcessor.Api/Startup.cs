using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradeProcessor.Repository;
using TradeProcessor.Services.Contracts;
using TradeProcessor.Services.Implementations;

namespace TradeProcessor.Api
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<TradeContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionString"]);
            });

            services.AddTransient<IInputValidatorService, InputValidatorServiceService>();
            services.AddTransient<IRegexService, RegexService>();
            services.AddTransient<ITradeParserService, TradeParserService>();
            services.AddTransient<ITradeDataService, TradeDataService>();
            services.AddTransient<ITradeRepository, TradeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
