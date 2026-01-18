using GestionDeInventario.Data;
using GestionDeInventario.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionDeInventario.Services;

public class EntradasService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Guardar(Entradas entrada)
    {
        if( !await Existe(entrada.EntradaId))
        {
            return await Insertar(entrada);
        }
        else
        {
            return await Modificar(entrada);
        }
    }
    private async Task<bool> Insertar(Entradas entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Entrada.Add(entrada);
        await AfectarExistencia(entrada.EntradaDetalles.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Entradas entrada)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        var newDate = await contexto.Entrada
            .Include(p => p.EntradaDetalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.EntradaId == entrada.EntradaId);
        if (newDate == null) { return false; }

        await AfectarExistencia(newDate.EntradaDetalles.ToArray(), TipoOperacion.Resta);

        contexto.Entrada.Update(entrada);
        await AfectarExistencia(entrada.EntradaDetalles.ToArray(), TipoOperacion.Suma);
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
    private async Task<bool> Existe(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entrada.AnyAsync(p => p.EntradaId == entradaId);
    }

    public async Task<List<Productos>>ListarProducto(Expression<Func<Productos, bool>> expression)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .Where(expression)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Entradas>>ListarEntradas(Expression<Func<Entradas, bool>>criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entrada
            .Include(p => p.EntradaDetalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool>Eliminar(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await contexto.Entrada
            .Include(p => p.EntradaDetalles)
            .FirstOrDefaultAsync(p => p.EntradaId == entradaId);

        if (entrada == null)
            return false;

        await AfectarExistencia(entrada.EntradaDetalles.ToArray(), TipoOperacion.Resta);
        contexto.EntradaDetalle.RemoveRange(entrada.EntradaDetalles);
        contexto.Entrada.Remove(entrada);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;

    }

    public async Task<Entradas?>Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entrada
            .Include(p => p.EntradaDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.EntradaId == id);
    }
    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }
}