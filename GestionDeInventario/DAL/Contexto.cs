using GestionDeInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionDeInventario.DAL;

public class Contexto: DbContext
{
    public DbSet<Entradas> Entrada { get; set; }
    public DbSet<EntradaDetalles> EntradaDetalle { get; set; }
    public DbSet<Productos> Producto { get; set; }
}
