
using Microsoft.OpenApi.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping.Aggregator", Version = "v1" });
            });

            builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogUrl"]!));

            builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]!));

            builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderingUrl"]!));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping.Aggregator v1"));
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}