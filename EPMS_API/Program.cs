using System.Text;
using EPMS_API;
using EPMS_API.Controllers;
using EPMS_API.Models;
using EPMS_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    // this defines a CORS policy called "default"
//    options.AddPolicy("default", policy =>
//    {
//        policy.WithOrigins("http://localhost:4000","https://epms.takasoap.xyz")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("token", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.OperationFilter<SecureEndpointAuthRequirementFilter>();
});

builder.Services.Configure<EpmsDatabaseSettings>(builder.Configuration.GetSection("EpmsDatabase"));
builder.Services.Configure<AuthorizationSettings>(builder.Configuration.GetSection("Authorization"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://epms-szu.authing.cn/oidc",
        ValidAudience = "62a35e7d54fab18ac73c5932",
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(""))
    };
});

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<CommentService>();
builder.Services.AddSingleton<RatingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("default");
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
