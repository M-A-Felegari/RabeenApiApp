using DataAccess;
using Microsoft.EntityFrameworkCore;
using RabeenApi.Factories;
using RabeenApi.Repositories;
using RabeenApi.Repositories.Implementations;
using RabeenApi.Services;
using RabeenApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ActionResultHandlersFactory>();

builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<AchievementService>();
builder.Services.AddScoped<IFileSaver, FileSaver>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();

builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
