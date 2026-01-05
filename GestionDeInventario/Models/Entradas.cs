using System.ComponentModel.DataAnnotations;

namespace GestionDeInventario.Models;

public class Entradas
{
    [Key]
    public int EntradaId { get; set; }
    [Required(ErrorMessage = "La fecha de la entrada es obligatoria.")]
    public DateTime Fecha { get; set; }
    [Required(ErrorMessage = "El concepto de la entrada es obligatorio.")]
    public string Concepto { get; set; }
    [Range(0.01, int.MaxValue, ErrorMessage ="El Total debe de ser mayor a 0.01")]
    public double Total { get; set; }

}
