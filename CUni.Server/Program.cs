using CUni.Data;
using CUni.Server.Config;
using CUni.Server.Data;
using CUni.Server.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<CUniContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CUniDb")));
builder.Services.AddDbContext<CUniContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CUniDb"));
    options.EnableSensitiveDataLogging();
});
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<CourseRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy => policy.AddPolicy("CorsPolicy", opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CUniContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
