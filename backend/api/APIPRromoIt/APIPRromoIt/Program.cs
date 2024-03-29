using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
       {
          {
              new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                     }
                   },
                       new string[]{}
           }
      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-h265dhdwa46lpcmn.us.auth0.com/";
    options.Audience = "dgwNLi968wdHkRnMzI6oLo2wo1KoDxve";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Admin"));
    options.AddPolicy("NPO Representative and Admin", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "NPO Representative", "Admin"));
    options.AddPolicy("Business representative and Admin", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Business representative", "Admin"));
    options.AddPolicy("Social Activist and Admin", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Social Activist", "Admin"));
    options.AddPolicy("Social Activist", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Social Activist"));
    options.AddPolicy("NPO Representative", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "NPO Representative"));
    options.AddPolicy("Business representative", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Business representative"));
});

var config = new ConfigurationBuilder()
    .AddJsonFile("C:/PromoIt/backend/config/Connection.json")
    .Build();

builder.Services.AddDbContext<APIPRromoIt.Models.promoitContext>(
    options =>
    {
        options.UseSqlServer(config.GetConnectionString("DotNetConnection"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
