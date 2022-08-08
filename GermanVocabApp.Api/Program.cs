using GermanVocabApp.DataAccess.EntityFramework;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VocabListDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("GermanVocabApp");
    options.UseSqlServer(connectionString);
    options.LogTo(Console.WriteLine);
    //StreamWriter sw = new StreamWriter("EfCoreLog.txt", append: true);
    //options.LogTo(sw.WriteLine);
    options.LogTo(log => Debug.WriteLine(log));
},
ServiceLifetime.Scoped);

builder.Services.AddScoped<IVocabListRepositoryAsync, VocabListRepositoryAsync>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
