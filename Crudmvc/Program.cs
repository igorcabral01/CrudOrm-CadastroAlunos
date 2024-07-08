using Crudmvc.Data;
using Crudmvc.Estudantes;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var conection = builder.Configuration.GetConnectionString("EstudantesConnection");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//criaçao do banco de dados 
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseMySql(conection,ServerVersion.AutoDetect(conection)));
// 

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();

app.AddRotasEsutantes();


app.Run();
