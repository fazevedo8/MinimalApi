using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Context;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();
//Adicionar as rotas

app.MapGet("/", () => "Catalogo de Produtos");

//utlizando o delegate conseguimos criar uma rota que recebe objeto 
//categoria e a injeção do contexto da base de dados
app.MapPost("/Categorias", async ([FromBody]Categoria categoria, AppDbContext db) =>
{
    db.Categorias.Add(categoria);
    await db.SaveChangesAsync();
    return Results.Created($"categorias/{categoria.CategoriaId}", categoria);

}
);

app.MapPut("/Categorias/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
{
    if (categoria.CategoriaId != id)
    {
        return Results.BadRequest();
    }

    var categoriaDb =  await db.Categorias.FindAsync(id);
    if (categoriaDb is not null)
    {
        categoriaDb = categoria;
        await db.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.BadRequest();

});

app.MapGet("/Categorias", async (AppDbContext db) => await db.Categorias.ToListAsync());

app.MapGet("/Categorias/{id:int}", async (int id, AppDbContext db)
    =>
{
    return await db.Categorias.FindAsync(id)
                is Categoria categoria ? Results.Ok(categoria)
                : Results.NotFound();
});

app.MapDelete("/Categorias/{id:int}", async (int id, AppDbContext db) =>
{
    var categoria = await db.Categorias.FindAsync(id);
    if (categoria is null) return Results.BadRequest();
    db.Categorias.Remove(categoria);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}