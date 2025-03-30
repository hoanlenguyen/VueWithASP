using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using webapi.Data;
using webapi.Mapper;
using webapi.Model.Identity;
using webapi.Model.Permission;
using webapi.Model.UserDto;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

//add db contexts
var connectionString = builder.Configuration.GetConnectionString("MyDatabase");
builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//add identity
builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

//add extra mapp config
MapperConfig.AddMapperConfigs();

//add CORS
var allowSpecificOriginsPolicy = "AllowSpecificOriginsPolicy";
var corsOrigins = (builder.Configuration["CorsOrigins"] ?? "http://localhost:3000").Split(',');
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOriginsPolicy, builder =>
    {
        builder.WithOrigins(corsOrigins)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((_) => true)
               .AllowCredentials();
    });
});

//add SignalR
//builder.Services.AddSignalR().AddHubOptions<HubClient>(options => options.EnableDetailedErrors = true);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add JWT configuration
//builder.Services
//    .AddAuthentication(option =>
//    {
//        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(option =>
//    {
//        option.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ClockSkew = TimeSpan.Zero
//        };
//    });

//add claims- permissions
builder.Services.AddAuthorization(options => options.AddCustomizedAuthorizationOptions());

// add Swagger & JWT authen to Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//var securityScheme = new OpenApiSecurityScheme()
//{
//    Name = "Authorization",
//    Type = SecuritySchemeType.ApiKey,
//    Scheme = "Bearer",
//    BearerFormat = "JWT",
//    In = ParameterLocation.Header,
//    Description = "JSON Web Token based security",
//};
//var securityReq = new OpenApiSecurityRequirement()
//{
//    {
//        new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference
//            {
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//            }
//        },
//        new string[] {}
//    }
//};
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(options =>
//{
//    //options.SwaggerDoc("v1", info);
//    options.AddSecurityDefinition("Bearer", securityScheme);
//    options.AddSecurityRequirement(securityReq);
//});

//add cache
builder.Services.AddMemoryCache();

//add DependencyInjection
//builder.Services.AddSingleton<ISendMailService, SendMailService>();
//builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
//builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
//builder.Services.AddSingleton<IMemoryCacheService>(provider => new MemoryCacheService(provider.GetService<IMemoryCache>(), mySQLConnection.ConnectionString));

//builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserPrincipal>(provider => new UserPrincipal(provider.GetService<IHttpContextAccessor>().HttpContext.User));

//add extra mapp config
//MapperConfig.AddMapperConfigs();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.DisplayRequestDuration();
    });
}

// CORS & Authen & Authorize
app.UseHttpsRedirection();
app.UseCors(allowSpecificOriginsPolicy);
//app.UseAuthentication();
//app.UseAuthorization();

//hub signalR
//app.MapHub<HubClient>("/hubClient");

//add Services
app.AddProductService();
app.AddAdminUserService();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var identityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    var storeDbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await DbInitializer.Initialize(identityDbContext, storeDbContext, userManager, roleManager);
}

app.Run();