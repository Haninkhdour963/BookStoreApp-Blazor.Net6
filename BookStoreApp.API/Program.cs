using BookStoreApp.API.Configurations;
using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConnStrings=builder.Configuration.GetConnectionString("BookStoreAppDbConnection");
builder.Services.AddDbContext<BooKstoreDbContext>(Options=>Options.UseSqlServer(ConnStrings));
//Add AutoMapper Configuration 
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//logging to track api
builder.Host.UseSerilog((ctx,lc)=>
lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

//Add Cores Configurations
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//use our Cores Policies
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
