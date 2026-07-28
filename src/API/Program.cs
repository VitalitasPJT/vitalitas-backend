using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text;
using API.Extensions;
using API.Middlewares;
using API.Settings;
using Application.Extensions;
using Infrastructure.Database.Context;
using Infrastructure.Database.Seed;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("Seção 'Jwt' ausente no appsettings.json.");
Validator.ValidateObject(jwtSettings, new ValidationContext(jwtSettings), validateAllProperties: true);

builder.Services.AddSingleton(Options.Create(jwtSettings));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

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
    options.SwaggerDoc("Aluno", new OpenApiInfo
    {
        Title = "Vitalitas API - Aluno",
        Version = "v1"
    });

    options.SwaggerDoc("Administrativo", new OpenApiInfo
    {
        Title = "Vitalitas API - Administrativo",
        Version = "v1"
    });

    options.SwaggerDoc("Gestor", new OpenApiInfo
    {
        Title = "Vitalitas API - Gestor",
        Version = "v1"
    });

    options.DocInclusionPredicate((documentName, apiDescription) =>
    {
        var groupName = apiDescription.GroupName;

        if (string.IsNullOrWhiteSpace(groupName))
        {
            return true;
        }

        return string.Equals(groupName, documentName, StringComparison.OrdinalIgnoreCase);
    });

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
            RoleClaimType = "Role",
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/Aluno/swagger.json", "Aluno");
        options.SwaggerEndpoint("/swagger/Administrativo/swagger.json", "Administrativo");
        options.SwaggerEndpoint("/swagger/Gestor/swagger.json", "Gestor");
    });

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<VitalitasDbContext>();
        dbContext.Database.Migrate();
        await DevelopmentSeeder.SeedAsync(dbContext);
    }
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<TenantResolutionMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
