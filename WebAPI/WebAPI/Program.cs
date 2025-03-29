
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Data;
using WebAPI.Repositories.Implementation;
using WebAPI.Repositories.Interface;

namespace WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("WebAPIConnectionString"));
			});

			builder.Services.AddSwaggerGen(options =>
			{
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});

			builder.Services.AddScoped<IExportRepository, ExportRepository>();

			var app = builder.Build();

			app.UseExceptionHandler(errorApp =>
			{
				errorApp.Run(async context =>
				{
					context.Response.StatusCode = 500;
					context.Response.ContentType = "application/json";

					var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
					if (exceptionHandlerPathFeature != null)
					{
						var errorResponse = new
						{
							Message = "Wystąpił błąd serwera.",
							Detailed = exceptionHandlerPathFeature.Error.Message
						};

						await context.Response.WriteAsJsonAsync(errorResponse);
					}
				});
			});

			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				db.Database.EnsureCreated();
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(options =>
			{
				options.AllowAnyHeader();
				options.AllowAnyOrigin();
				options.AllowAnyMethod();
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();


			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}
