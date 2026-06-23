using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

 

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<AppDbContaxt>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));
 
builder.Services.AddOpenApi();

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
