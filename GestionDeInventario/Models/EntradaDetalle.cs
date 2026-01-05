using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeInventario.Models;

public class EntradaDetalle
{
    [Key]
    public int Id { get; set; }
    public int EntradaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public double Costo { get; set; }

    [ForeignKey("ProductoId")]
    public virtual ICollection<Producto> Productos { get; set; }

    [ForeignKey("EntradaId")]
    public virtual ICollection<Entrada> Entradas { get; set; }

}
