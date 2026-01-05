using GestionDeInventario.DAL;
using GestionDeInventario.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDeInventario.Services;

public class ProductoServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Productos productos)
    {
        if( !await Existe(productos.ProductoId))
        {
            return await Insertar(productos);
        }
        else
        {
            return await Modificar(productos);
        }
    }
    private async Task<bool> Insertar(Productos productos)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Producto.Add(productos);
        return await contexto.SaveChangesAsync() > 0;

    }
    private async Task<bool> Modificar(Productos productos)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        var newDate = await contexto.Producto
            .Include(p => p.EntradaDetalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.ProductoId == productos.ProductoId);
        if (newDate == null) { return false; }


        contexto.Producto.Update(productos);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task AfectarExistencia(EntradaDetalles[] entradaDetalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach(var detalle in entradaDetalle)
        {
            var producto = await contexto.Producto.FindAsync(detalle.ProductoId);
            if(tipoOperacion == TipoOperacion.Resta)
                producto.Existencia -= detalle.Cantidad;
            else
                producto.Existencia += detalle.Cantidad;
            await contexto.SaveChangesAsync();
            }
        }
    private async Task<bool> Existe(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto.AnyAsync(p => p.ProductoId == productoId);
    }

    public async Task<List<Productos>>ListarProducto(Expression<Func<Productos, bool>> expression)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .Where(expression)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool>Eliminar(int productoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var producto = await contexto.Producto
            .Include(p => p.EntradaDetalles)
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);

        if (producto == null)
            return false;

        await AfectarExistencia(producto.EntradaDetalles.ToArray(), TipoOperacion.Resta);
        contexto.EntradaDetalle.RemoveRange(producto.EntradaDetalles);
        contexto.Producto.Remove(producto);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;

    }

    public async Task<Productos>Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .Include(p => p.EntradaDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductoId == id);
    }
    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }
}