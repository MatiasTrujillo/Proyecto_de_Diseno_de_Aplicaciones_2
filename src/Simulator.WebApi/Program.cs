using Microsoft.EntityFrameworkCore;
using Simulator.BussinesLogic;
using Simulator.DataAcces;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Simulator.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers(options => { options.Filters.Add(typeof(ExceptionFilter)); });
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IMethodRepository, MethodRepository>();

builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IMethodService, MethodService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
