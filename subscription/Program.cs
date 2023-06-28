using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using subscription.Extension;
using System.Text;
using subscription.repositories;
using subscription.services;
using subscription.models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration Configuration = builder.Configuration;
string ConnectionStrings = Configuration.GetConnectionString("Default");
//builder.Services.AddDbContext<SubscriptionContext>(options => options.UseNpgsql(ConnectionStrings));
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<SubscriptionContext>(options =>
  options.UseNpgsql(Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();
builder.Services.AddServiceRepositories();
builder.Services.AddService();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration["JwtAuthentication:JwtIssuer"],
        ValidAudience = Configuration["JwtAuthentication:JwtIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthentication:JwtKey"])),
    };

});
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "SubscriptionApi", Version = "v1" });
    var xmlFile = Path.ChangeExtension(typeof(Program).Assembly.Location, ".xml");
    setup.IncludeXmlComments(xmlFile);
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });

});
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
          .SetPreflightMaxAge(TimeSpan.FromDays(3600))
           ;


}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuth();
app.UseAuthorization();
app.UseCors("MyPolicy");
app.MapControllers();

app.Run();
