using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSRFInstaClone.WebAPI;

public static class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		Program.ConfigureServiceCollection(builder);
		var app = builder.Build();
		Program.ConfugurePipeline(app);
		app.Run();
	}

	private static void ConfigureServiceCollection(WebApplicationBuilder builder) {
		Program.ConfigureControllers(builder);
		Program.ConfigureSwagger(builder);
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
}