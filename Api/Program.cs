using Api.Data;
using Microsoft.EntityFrameworkCore;

const string CORS_POLICY_NAME = "MyCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=MusicOrganiser.db"));
builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS_POLICY_NAME, policy =>
        policy.WithOrigins("https://localhost:7293")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(CORS_POLICY_NAME);
app.MapControllers();

app.Run();
