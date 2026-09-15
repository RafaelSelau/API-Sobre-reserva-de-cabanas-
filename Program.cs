var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Lista em memória para armazenar os registros das cabanas
List<Cabana> cabanas = new()
{
    new Cabana(
        Id: 1,
        Nome: "Cabana Vista da Serra",
        Localizacao: "Campos do Jordão, SP",
        PrecoPorNoite: 450.00m,
        CapacidadeHospedes: 4,
        Disponivel: true
    ),
    new Cabana(
        Id: 2,
        Nome: "Cabana Lago Azul",
        Localizacao: "Gramado, RS",
        PrecoPorNoite: 620.00m,
        CapacidadeHospedes: 6,
        Disponivel: true
    )
};

// Rota raiz: Confirmação de API online
app.MapGet("/", () => Results.Ok(new
{
    mensagem = "API de Reserva de Cabanas está no ar!",
    status = "Online",
    versao = "1.0"
}));

// GET /api/cabanas - Lista todos os registros
app.MapGet("/api/cabanas", () => Results.Ok(cabanas));

// GET /api/cabanas/{id} - Busca um registro por ID
app.MapGet("/api/cabanas/{id:int}", (int id) =>
{
    var cabana = cabanas.FirstOrDefault(c => c.Id == id);
    if (cabana is null)
    {
        return Results.NotFound(new { mensagem = $"Cabana com ID {id} não encontrada." });
    }

    return Results.Ok(cabana);
});

// POST /api/cabanas - Cadastra um novo registro
app.MapPost("/api/cabanas", (CabanaInputDto dto) =>
{
    int novoId = cabanas.Count > 0 ? cabanas.Max(c => c.Id) + 1 : 1;

    var novaCabana = new Cabana(
        novoId,
        dto.Nome,
        dto.Localizacao,
        dto.PrecoPorNoite,
        dto.CapacidadeHospedes,
        dto.Disponivel
    );

    cabanas.Add(novaCabana);

    return Results.Created($"/api/cabanas/{novaCabana.Id}", novaCabana);
});

// PUT /api/cabanas/{id} - Atualiza um registro existente
app.MapPut("/api/cabanas/{id:int}", (int id, CabanaInputDto dto) =>
{
    var index = cabanas.FindIndex(c => c.Id == id);
    if (index == -1)
    {
        return Results.NotFound(new { mensagem = $"Cabana com ID {id} não encontrada para atualização." });
    }

    var cabanaAtualizada = new Cabana(
        id,
        dto.Nome,
        dto.Localizacao,
        dto.PrecoPorNoite,
        dto.CapacidadeHospedes,
        dto.Disponivel
    );

    cabanas[index] = cabanaAtualizada;

    return Results.Ok(cabanaAtualizada);
});

// DELETE /api/cabanas/{id} - Remove um registro existente
app.MapDelete("/api/cabanas/{id:int}", (int id) =>
{
    var cabana = cabanas.FirstOrDefault(c => c.Id == id);
    if (cabana is null)
    {
        return Results.NotFound(new { mensagem = $"Cabana com ID {id} não encontrada para remoção." });
    }

    cabanas.Remove(cabana);

    return Results.NoContent();
});

app.Run();

// Records (DTOs e Modelo do Recurso Principal)
public record Cabana(
    int Id,
    string Nome,
    string Localizacao,
    decimal PrecoPorNoite,
    int CapacidadeHospedes,
    bool Disponivel
);

public record CabanaInputDto(
    string Nome,
    string Localizacao,
    decimal PrecoPorNoite,
    int CapacidadeHospedes,
    bool Disponivel
);
