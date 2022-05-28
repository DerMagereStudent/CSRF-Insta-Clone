using System;
using System.Linq;
using System.Threading.Tasks;

using CSRFInstaClone.Infrastructure.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSRFInstaClone.WebAPI;

public static class Program {
	public static async Task Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		Program.ConfigureServiceCollection(builder);
		var app = builder.Build();
		Program.ConfugurePipeline(app);
		
		using (var scope = app.Services.CreateScope()) {
			var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			
			if ((await context.Database.GetPendingMigrationsAsync()).Any())
				await context.Database.MigrateAsync();
		}
		
		await app.RunAsync();
	}

	private static void ConfigureServiceCollection(WebApplicationBuilder builder) {
		Program.ConfigureControllers(builder);
		Program.ConfigureSwagger(builder);
		Program.ConfigureDatabase(builder);
	}

	private static void ConfugurePipeline(WebApplication app) {
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();
	}

	private static void ConfigureControllers(WebApplicationBuilder builder) {
		builder.Services.AddControllers();
	}

	private static void ConfigureSwagger(WebApplicationBuilder builder) {
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
	}
	
	private static void ConfigureDatabase(WebApplicationBuilder builder) {
		builder.Services.AddDbContext<ApplicationDbContext>(
			(serviceProvider, optionsBuilder) => optionsBuilder
				.UseNpgsql(
					builder.Configuration.GetConnectionString("postgres")
						.Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER"))
						.Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"))
						.Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB")),
					optionsBuilder => {
						optionsBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
					}
				)
		);
	}
}