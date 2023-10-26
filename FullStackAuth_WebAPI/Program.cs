using FullStackAuth_WebAPI.ActionFilters;
using FullStackAuth_WebAPI.Contracts;
using FullStackAuth_WebAPI.Extensions;
using FullStackAuth_WebAPI.Managers;
using FullStackAuth_WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FullStackAuth_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Existing service registrations...
            builder.Services.ConfigureCors();
            builder.Services.ConfigureMySqlContext(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<ValidationFilterAttribute>();
            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            builder.Services.AddScoped<FoodItemService>();
            builder.Services.AddControllers();

         
            builder.Services.AddHttpClient<GooglePlacesService>(client =>
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/");
                
            });

            // Swagger configuration...
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Existing middleware configurations...
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
