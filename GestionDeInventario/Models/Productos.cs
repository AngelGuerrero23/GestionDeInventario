using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeInventario.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    public string Descripcion { get; set; } 
    [Required(ErrorMessage = "El costo del producto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que cero.")]
    public double Costo { get; set; }
    [Required(ErrorMessage = "El precio del producto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa.")]
    public int Existencia { get; set; }

    
}

