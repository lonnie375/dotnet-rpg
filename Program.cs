global using dotnet_rpg.Services.CharacterService;
global using dotnet_rpg.Models; 
global using dotnet_rpg.Dtos.Character;
global using Microsoft.EntityFrameworkCore;
using dotnet_rpg.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"))
);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Instruct the controller on which services to use 
builder.Services.AddScoped<ICharacterService, CharacterService>();

//to Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
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
