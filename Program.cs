using Microsoft.EntityFrameworkCore;
using ApiCabanas.Data;
using ApiCabanas.Models;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Aplicar migrations automaticamente ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Servir arquivos estáticos do wwwroot (frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

// GET / — Confirmação de que a API está no ar
app.MapGet("/", () => Results.Ok(new
{
    mensagem = "API de Reserva de Cabanas está no ar!",
    status = "Online",
    versao = "2.0"
}));

// GET /api/cabanas — Lista todos os registros
app.MapGet("/api/cabanas", async (AppDbContext db) =>
    Results.Ok(await db.Cabanas.ToListAsync()));

// GET /api/cabanas/{id} — Busca um registro por ID
app.MapGet("/api/cabanas/{id:int}", async (int id, AppDbContext db) =>
{
    var cabana = await db.Cabanas.FindAsync(id);
    if (cabana is null)
        return Results.NotFound(new { mensagem = $"Cabana com ID {id} não encontrada." });

    return Results.Ok(cabana);
});

// POST /api/cabanas — Cadastra um novo registro
app.MapPost("/api/cabanas", async (CabanaInputDto dto, AppDbContext db) =>
{
    var cabana = new Cabana
    {
        Nome = dto.Nome,
        Localizacao = dto.Localizacao,
        PrecoPorNoite = dto.PrecoPorNoite,
        CapacidadeHospedes = dto.CapacidadeHospedes,
        Disponivel = dto.Disponivel
    };

    db.Cabanas.Add(cabana);
    await db.SaveChangesAsync();

    return Results.Created($"/api/cabanas/{cabana.Id}", cabana);
});

// PUT /api/cabanas/{id} — Atualiza um registro existente
app.MapPut("/api/cabanas/{id:int}", async (int id, CabanaInputDto dto, AppDbContext db) =>
{
    var cabana = await db.Cabanas.FindAsync(id);
    if (cabana is null)
        return Results.NotFound(new { mensagem = $"Cabana com ID {id} não encontrada para atualização." });

    cabana.Nome = dto.Nome;
    cabana.Localizacao = dto.Localizacao;
    cabana.PrecoPorNoite = dto.PrecoPorNoite;
    cabana.CapacidadeHospedes = dto.CapacidadeHospedes;
    cabana.Disponivel = dto.Disponivel;

    await db.SaveChangesAsync();

    return Results.Ok(cabana);
});

// DELETE /api/cabanas/{id} — Remove um registro existente
app.MapDelete("/api/cabanas/{id:int}", async (int id, AppDbContext db) =>
{
    var cabana = await db.Cabanas.FindAsync(id);
    if (cabana is null)
        return Results.NotFound(new { mensagem = $"Cabana com ID {id} não encontrada para remoção." });

    db.Cabanas.Remove(cabana);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

// DTO de entrada (sem o Id — gerado automaticamente pelo banco)
public record CabanaInputDto(
    string Nome,
    string Localizacao,
    decimal PrecoPorNoite,
    int CapacidadeHospedes,
    bool Disponivel
);
