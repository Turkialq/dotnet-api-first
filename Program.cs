using dotnet_api_first.Configurations.Filters;
using dotnet_api_first.Data;
using dotnet_api_first.Services.BackgroundJobs;
using dotnet_api_first.Services.Chache;
using dotnet_api_first.Services.CharacterService;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHangfire(config =>
config.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DbContext")));
builder.Services.AddHangfireServer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme.""",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddTransient<IBackground, Background>();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(2);
        opt.PermitLimit = 10;
        opt.QueueLimit = 15;
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;

    }).RejectionStatusCode = 429; // --> too many request
});
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddDbContext<DataContex>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DbContext")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHangfireDashboard();

app.MapHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<IBackground>(x => x.BackUpDatabase(), Cron.Minutely); // --> update database every day

app.Run();
