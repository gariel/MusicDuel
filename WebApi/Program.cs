using Application;
using Infra;
using WebApi;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectApplication();
builder.Services.InjectInfra();
builder.Services.InjectWebApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthorizationMiddleware>();
app.MapControllers();
app.Run();