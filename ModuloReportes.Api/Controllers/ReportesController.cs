using Microsoft.AspNetCore.Mvc;
using MóduloProductos.Api.Models;
using ModuloMovimientos.Api.Models;


namespace ModuloReportes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        // Simulamos una lista de productos local
        private static readonly List<Producto> _productos = new()
        {
            new Producto { Id = 1, Nombre = "Laptop", Cantidad = 10 },
            new Producto { Id = 2, Nombre = "Mouse", Cantidad = 50 },
            new Producto { Id = 3, Nombre = "Teclado", Cantidad = 30 }
        };

        // Pero los movimientos vienen del módulo de movimientos
        private static readonly List<Movimiento> _movimientos = new()
        {
            new Movimiento { Id = 1, ProductoId = 1, Cantidad = 2, Tipo = TipoMovimiento.Salida, Fecha = DateTime.Now.AddDays(-2) },
            new Movimiento { Id = 2, ProductoId = 2, Cantidad = 5, Tipo = TipoMovimiento.Salida, Fecha = DateTime.Now.AddDays(-1) },
            new Movimiento { Id = 3, ProductoId = 2, Cantidad = 10, Tipo = TipoMovimiento.Entrada, Fecha = DateTime.Now },
            new Movimiento { Id = 4, ProductoId = 3, Cantidad = 1, Tipo = TipoMovimiento.Salida, Fecha = DateTime.Now.AddDays(-5) }
        };

        // GET /api/reportes/stock-actual
        [HttpGet("stock-actual")]
        public ActionResult<IEnumerable<Producto>> ObtenerStockActual()
        {
            return Ok(_productos);
        }

        // GET /api/reportes/movimientos-por-producto/{id}
        [HttpGet("movimientos-por-producto/{id}")]
        public ActionResult<IEnumerable<Movimiento>> ObtenerMovimientosPorProducto(int id)
        {
            var movimientos = _movimientos.Where(m => m.ProductoId == id).ToList();

            if (!movimientos.Any())
                return NotFound($"No hay movimientos para el producto con Id {id}");

            return Ok(movimientos);
        }

        // GET /api/reportes/productos-mas-vendidos
        [HttpGet("productos-mas-vendidos")]
        public ActionResult<IEnumerable<object>> ObtenerProductosMasVendidos()
        {
            var vendidos = _movimientos
                .Where(m => m.Tipo == TipoMovimiento.Salida)
                .GroupBy(m => m.ProductoId)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    TotalSalidas = g.Sum(x => x.Cantidad),
                    ProductoNombre = _productos.FirstOrDefault(p => p.Id == g.Key)?.Nombre ?? "Desconocido"
                })
                .OrderByDescending(x => x.TotalSalidas)
                .ToList();

            return Ok(vendidos);
        }
    }
}
