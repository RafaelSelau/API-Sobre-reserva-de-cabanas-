namespace ApiCabanas.Models;

public class Cabana
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Localizacao { get; set; } = string.Empty;
    public decimal PrecoPorNoite { get; set; }
    public int CapacidadeHospedes { get; set; }
    public bool Disponivel { get; set; }
}
