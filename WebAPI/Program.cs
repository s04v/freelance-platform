using Core.Chat;
using Core.Common.Behaviors;
using Core.Jobs;
using Core.Orders;
using Core.Services;
using Core.Users;
using Database;
using Database.Users;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI.Filters;
using WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("secrets.json");

var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

// Add Services
builder.Services.AddSingleton<IMailSender, MailSender>();
builder.Services.AddSingleton<IMailService, MailService>();

// Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add Mediatr
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


builder.Services.AddHealthChecks();

if (builder.Environment.IsProduction())
{
    builder.Services.AddAzureClients(clientBuilder =>
    {
        clientBuilder.AddBlobServiceClient(builder.Configuration["Azure:BlobStorage:ConnectionString"]);
    });

    builder.Services.AddScoped<IFileStorage, AzureFileStorage>();

} 
else
{
    builder.Services.AddScoped<IFileStorage, LocalFileStorage>();
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false
    };
    
    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        },
    };
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ExceptionFilter());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
       .Enrich.FromLogContext()
       .WriteTo.Console(new RenderedCompactJsonFormatter())
       .CreateLogger();

builder.Services.AddDbContext<BaseDbContext>(opt =>
{
    opt
        .UseSqlServer(connectionString)
        .LogTo(Log.Logger.Information, LogLevel.Information, null);
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireClaim("id");
    });
});

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("*");
        });
});
*/
builder.Services.AddSignalR().AddHubOptions<ChatHub>(opt =>
{
    opt.EnableDetailedErrors = true;
    opt.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
    opt.HandshakeTimeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Configuration["FileStorage:LocalRootPath"], "photo")
            ),
        RequestPath = "/Static/Avatar"
    });
}
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
app.UseRouting();

/*app.UseCors("AllowAllOrigins");*/

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

app.MapControllers();

app.Run();
