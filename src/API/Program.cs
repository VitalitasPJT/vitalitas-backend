using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ValidateJwtConfiguration(builder.Configuration);

builder.Services.AddSingleton<Vitalitas.Infrastructure.Database.Connection.DbConnectionFactory>();
builder.Services.AddScoped<Domain.Interfaces.IUsuario, Infrastructure.Persistence.UsuarioRepository>();
builder.Services.AddScoped<Application.Interfaces.IUsuarioUseCase, Application.Services.UsuarioUC>();
builder.Services.AddScoped<Vitalitas.Backend.API.Services.JwtService.IJwtService, Vitalitas.Backend.API.Services.JwtService.JwtService>();

/*builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Use o header Authorization com o formato: Bearer {token}"
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
//Console.WriteLine("JWT KEY (DEBUG): " + builder.Configuration["Jwt:Key"]);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

static void ValidateJwtConfiguration(IConfiguration configuration)
{
    var jwtKey = configuration["Jwt:Key"];
    var jwtIssuer = configuration["Jwt:Issuer"];
    var jwtAudience = configuration["Jwt:Audience"];
    var jwtDuration = configuration["Jwt:DurationInMinutes"];

    if (string.IsNullOrWhiteSpace(jwtKey))
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:Key' is missing or empty. Configure it in the API project settings or user-secrets.");
    }

    if (string.IsNullOrWhiteSpace(jwtIssuer))
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:Issuer' is missing or empty.");
    }

    if (string.IsNullOrWhiteSpace(jwtAudience))
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:Audience' is missing or empty.");
    }

    if (!int.TryParse(jwtDuration, out var durationInMinutes) || durationInMinutes <= 0)
    {
        throw new InvalidOperationException(
            "JWT configuration is invalid: 'Jwt:DurationInMinutes' must be a positive integer.");
    }
}
