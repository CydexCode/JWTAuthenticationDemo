using JWTAuthenticationDemo.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.AccessControl;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication("JWTAuth")
     .AddJwtBearer("JWTAuth", options =>
     {

         var keyBytes = Encoding.UTF8.GetBytes(Constants.Secret);
         var key = new SymmetricSecurityKey(keyBytes);

         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidIssuer = Constants.Issuer,
             ValidAudience = Constants.Audience,
             IssuerSigningKey = key
         };

     
     });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
