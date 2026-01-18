using GestionDeInventario.Data;
using GestionDeInventario.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDeInventario.Services;

public class ProductoService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<Productos>Buscar(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<List<Productos>>Listar(Expression<Func<Productos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

   public async Task<Productos>Guardar(Productos producto)
    {
        if(!await Existe(producto.ProductoId))
        {
            return await Insertar(producto);
        }
        else
        {
            return await Modificar(producto);
        }
    }

    public async Task<Productos>Insertar(Productos producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Producto.Add(producto);
        await contexto.SaveChangesAsync();
        return producto;
    }
    public async Task<bool>Existe(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .AnyAsync(p => p.ProductoId == productoId);
    }
    public async Task<bool>Eliminar(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var producto = await contexto.Producto
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);
        if(producto == null)
        {
            return false;
        }
        contexto.Producto.Remove(producto);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<Productos>Modificar(Productos producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Producto.Update(producto);
        await contexto.SaveChangesAsync();
        return producto;
    }
}
