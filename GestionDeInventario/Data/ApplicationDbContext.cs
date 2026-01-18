using GestionDeInventario.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionDeInventario.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
        public DbSet<Entradas> Entrada { get; set; }
        public DbSet<EntradaDetalles> EntradaDetalle { get; set; }
        public DbSet<Productos> Producto { get; set; }
 
}