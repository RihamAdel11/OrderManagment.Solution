
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderManagmentSystem.Core;
using OrderManagmentSystem.Core.Entites;
using OrderManagmentSystem.Core.Identity;
using OrderManagmentSystem.Core.Repositries;
using OrderManagmentSystem.Core.Services;
using OrderManagmentSystem.Core.Services.EmailService;
using OrderManagmentSystem.Core.Services.PaymentServices;
using OrderManagmentSystem.Helper;
using OrderManagmentSystem.MiddleWare;
using OrderManagmentSystem.Services;
using OrderMangmentSystem.Repositry;
using OrderMangmentSystem.Repositry.Data;
using OrderMangmentSystem.Repositry.Identity;
using System.Text;

namespace OrderManagmentSystem
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			builder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuser"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:MySecuredAPIUser"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"] ?? string.Empty))
				,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
				};
			});
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IOrderServices, OrderServices>();
			builder.Services.AddScoped<IAuthServices, AuthServices>();
			builder.Services.AddScoped<IPaymentServices, PaymentServices>();
			builder.Services.AddIdentity<User, ApplicationRole>()
		.AddEntityFrameworkStores<AppIdentityDbContext >()
		.AddDefaultTokenProviders();

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminPolicy", policy =>
					policy.RequireRole("Admin"));

				options.AddPolicy("ManagerPolicy", policy =>
					policy.RequireRole("Manager"));

				options.AddPolicy("CustomerPolicy", policy =>
					policy.RequireRole("Customer"));
			});

			builder.Services.AddScoped<IEmailServices, SmtpEmailService>(provider =>
	   new SmtpEmailService("smtp.example.com", 587, "username", "password"));
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderMangmentSystem", Version = "v1" });
			});
			var app = builder.Build();
			#region Update-DataBase



			using var scope = app.Services.CreateScope();

			var servies = scope.ServiceProvider;


			var loggerFactory = servies.GetRequiredService<ILoggerFactory>();
			try
			{
				var _dbContext = servies.GetRequiredService<StoreContext>();
				await _dbContext.Database.MigrateAsync();
				var IdentitydbContext = servies.GetRequiredService<AppIdentityDbContext>();
				await IdentitydbContext.Database.MigrateAsync();

			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, " an Error has Occure During Update DataBase");


			}
			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{

				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderMangmentSystem v1"));
				app.UseStatusCodePagesWithRedirects("/errors/{0}");

				app.UseHttpsRedirection();
				app.UseAuthentication();

				app.UseAuthorization();


				app.MapControllers();

				app.Run();
			}
		}
	}
}
