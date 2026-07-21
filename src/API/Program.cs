using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ValidateJwtConfiguration(builder.Configuration);

builder.Services.AddSingleton<Infrastructure.Database.Connections.DbConnectionFactory>();
builder.Services.AddScoped<Domain.Features.Usuarios.Common.Interfaces.IUsuarioRepository, Infrastructure.Repositories.UsuarioRepository>();
builder.Services.AddScoped<Application.Usuarios.Common.Interfaces.IUsuarioUseCase, Application.Usuarios.Common.UseCases.UsuarioUC>();
builder.Services.AddScoped<Domain.Features.Usuarios.Aluno.Interfaces.IAlunoRepository, Infrastructure.Repositories.AlunoRepository>();
builder.Services.AddScoped<Application.Usuarios.Aluno.Interfaces.IAlunoUseCase, Application.Usuarios.Aluno.UseCases.AlunoUC>();
builder.Services.AddScoped<Domain.Features.Usuarios.Gestor.Interfaces.IGestorRepository, Infrastructure.Repositories.GestorRepository>();
builder.Services.AddScoped<Application.Usuarios.Gestor.Interfaces.IGestorUseCase, Application.Usuarios.Gestor.UseCases.GestorUC>();
builder.Services.AddScoped<API.Services.IJwtService, API.Services.JwtService>();
builder.Services.AddScoped<Application.Token.Service.ITokenService, API.Services.JwtService>();
builder.Services.AddSingleton(new Application.Token.Settings.RefreshTokenSettings(
    int.Parse(builder.Configuration["Jwt:RefreshTokenDurationInDays"] ?? "7")));
builder.Services.AddScoped<Domain.Features.Token.Interfaces.IRefreshTokenRepository, Infrastructure.Repositories.RefreshTokenRepository>();
builder.Services.AddScoped<Application.Token.Interfaces.IRefreshTokenUseCase, Application.Token.UseCases.RefreshTokenUC>();
builder.Services.AddScoped<Domain.Features.Fichas.FichaMedica.Interfaces.IFichaMedicaRepository, Infrastructure.Repositories.FichaMedicaRepository>();
builder.Services.AddScoped<Application.Fichas.FichaMedica.Interfaces.IFichaMedicaUseCase, Application.Fichas.FichaMedica.UseCases.FichaMedicaUC>();


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
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
//Console.WriteLine("JWT KEY (DEBUG): " + builder.Configuration["Jwt:Key"]);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/Aluno/swagger.json", "Aluno");
        options.SwaggerEndpoint("/swagger/Administrativo/swagger.json", "Administrativo");
        options.SwaggerEndpoint("/swagger/Gestor/swagger.json", "Gestor");
    });
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
