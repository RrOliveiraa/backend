using FlavorsOfOliveira.Data.Context;
using FlavorsOfOliveira.Repository.Implementations;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Services.Implementations;
using FlavorsOfOliveira.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;


namespace FlavorsOfOliveira
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			ConfigureServices(builder.Services);

			var app = builder.Build();

			ConfigureApp(app);
		}
		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddDbContext<FlavorsOfOliveiraDBContext>();

			services.AddScoped<IAdminRepository, AdminRepository>();
			services.AddScoped<IRecipeRepository, RecipeRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserRecipeRepository, UserRecipeRepository>();

			services.AddScoped<IAdminService, AdminService>();
			services.AddScoped<IRecipeService, RecipeService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRecipeService, UserRecipeService>();

			services.AddCors(options => options.AddPolicy("corspolicy", policy =>
			{
				policy.AllowAnyHeader()
										.AllowAnyMethod()
										.WithOrigins("http://localhost:4200") // URL do frontend Angular
										.AllowCredentials(); // Permitir cookies
			}));

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("FlavorsOfOliveiraApi", new OpenApiInfo
				{
					Title = "FlavorsOfOliveira Api",
					Version = "1.0"
				});
			});
		}

		private static void ConfigureApp(WebApplication app)
		{

			app.UseHttpsRedirection();
			app.UseRouting();
   app.UseAuthentication();
   app.UseCors("corspolicy");
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger().UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("FlavorsOfOliveiraApi/Swagger.json", "FlavorsOfOliveira Api");
    
			});

			app.Run();
		}
	}
}
