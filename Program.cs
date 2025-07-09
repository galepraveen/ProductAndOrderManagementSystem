
using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace InventoryAndOrderManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // linking the created DBContext with builder
            //------------------------------------------------------------------------------------------------------
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseInMemoryDatabase("InventoryOrderManagement");
            });
            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Dependency Injection
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(kvp => kvp.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var result = new
                    {
                        message = "Validation failed.",
                        errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });


            //------------------------------------------------------------------------------------------------------

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
