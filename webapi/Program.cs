using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Mapper;
using webapi.Model.Identity;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

//add db context
var connectionString = "Data Source=localsql;Initial Catalog=DemoDB;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//add identity
builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

//add extra mapp config
MapperConfig.AddMapperConfigs();

//add CORS
//var allowSpecificOriginsPolicy = "AllowSpecificOriginsPolicy";
//var corsOrigins = (builder.Configuration["CorsOrigins"] ?? "http://localhost:8080").Split(',');
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: allowSpecificOriginsPolicy, builder =>
//    {
//        builder.WithOrigins(corsOrigins)
//               .AllowAnyHeader()
//               .AllowAnyMethod()
//               .SetIsOriginAllowed((_) => true)
//               .AllowCredentials();
//    });
//});

//add SignalR
//builder.Services.AddSignalR().AddHubOptions<HubClient>(options => options.EnableDetailedErrors = true);

// Set the JSON serializer options
//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//});

//// Add JWT configuration
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
//            IssuerSigningKey = new SymmetricSecurityKey
//                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ClockSkew = TimeSpan.Zero
//        };
//    });

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
//builder.Services.AddTransient<IUserPrincipal>(provider => new UserPrincipal(provider.GetService<IHttpContextAccessor>().HttpContext.User));

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
//app.UseCors(allowSpecificOriginsPolicy);
//app.UseAuthentication();
//app.UseAuthorization();

//hub signalR
//app.MapHub<HubClient>("/hubClient");

//add Services
app.AddProductService();

if(app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await DbInitializer.Initialize(dbInitializer, userManager);
    }
}



app.Run();