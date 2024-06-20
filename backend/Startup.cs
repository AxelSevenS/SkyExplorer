using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace SkyExplorer;

public class Startup(IConfiguration Configuration) {
	public void ConfigureServices(IServiceCollection services) {
		JwtOptions jwtOptions = Configuration.GetSection(JwtOptions.Jwt)
			.Get<JwtOptions>()!;
		services.AddSingleton(jwtOptions);


		services.AddDbContext<AppDbContext>(
			opt => {
				opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
			}
		);


		services.AddControllers();

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(options => {
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
				In = ParameterLocation.Header,
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					[]
				}
			});

			string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
		});


		services.AddAuthentication(options => {
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options => {
#if DEBUG
				options.RequireHttpsMetadata = false;
#else
				options.RequireHttpsMetadata = true;
#endif

				options.MapInboundClaims = false;

				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters {
					ClockSkew = TimeSpan.Zero,

					ValidateAudience = true,
					ValidAudience = jwtOptions.Audience,

					ValidateIssuer = true,
					ValidIssuer = jwtOptions.Issuer,

					ValidateLifetime = true,

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = jwtOptions.SecurityKey
				};
			});

		services.AddAuthorizationBuilder()
			.AddDefaultPolicy("Authenticated", policy => {
				// policy.RequireAssertion(context => true);
				policy.RequireAuthenticatedUser();
				policy.RequireClaim(JwtRegisteredClaimNames.Sub);
				policy.RequireClaim(JwtOptions.RoleClaim);
			});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
		app.UseSwaggerUI(options => {
			options.SwaggerEndpoint($"../swagger/v1/swagger.json", "v1");
			options.RoutePrefix = "api-docs";
		});


		if (env.IsDevelopment()) {
			app.UseDeveloperExceptionPage();
		}

		app.UseCors(builder => builder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
		);

		app.UseHttpsRedirection();
		app.UseStaticFiles(
			new StaticFileOptions {
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "swagger")),
				RequestPath = "/swagger"
			}
		);


		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints => {
			endpoints.MapControllers();
		});
	}
}