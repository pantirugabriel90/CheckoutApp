using CheckoutApp.Kafka;
using CheckoutApp.Models.Domain;
using CheckoutApp.Repositories;
using CheckoutApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutApp
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
            services.AddEntityFrameworkSqlServer();
            services.AddControllers();
            services.AddScoped<IEventProcessor, EventProcessor>();
            services.AddScoped<IBasketQueryHandler, BasketQueryHandler>();
            services.AddScoped<ICustomLogger, Logger>();
            services.AddScoped<ICustomLogger, Logger>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            services.AddDbContextPool<BasketContext>(options => options.UseSqlServer(connectionString));

            // kafka logic not finalized, the app works syncronously for now
            services.AddScoped<IProducer, Producer>();

            //services.AddScoped<IConsumer, Consumer>();
            //TO DO:: register kafka consummer as hosted service to run in background and listen to events

            //(options =>
            //options.UseSqlServer(Configuration.GetConnectionString(connectionString)));

            //using (var context = new BasketContext())
            //{

            //    var std = new Article()
            //    {
            //        Name = "Bill"
            //    }; var std2 = new Article()
            //    {
            //        Name = "Billaaaaaaaaa"
            //    };
            //    var bsk = new Basket()
            //    {
            //        Customer = "me",
            //        PaysVAT = true,
            //        Articles = new List<Article> { std2, std }
            //    };
            //    context.Baskets.Add(bsk);
            //    context.SaveChanges();
            //}
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
