
using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Middlewares;
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


            var useIMemory = builder.Configuration.GetValue<bool>("UseInMemoryDb");
            if (useIMemory)
            {
                builder.Services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InventoryOrderManagement");
                });
            }
            else
            {
                builder.Services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDB"));
                });
            }
            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Dependency Injection
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddExceptionHandler<AppExceptionHandler>(); // registering the app exception handler dependency
            builder.Services.AddProblemDetails();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
