using Microsoft.AspNetCore.Mvc;
using MóduloProductos.Api.Models;

namespace MóduloProductos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private static List<Producto> _productos = new();

        private static int _nextId = 1;

        // POST /api/productos
        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            producto.Id = _nextId++;
            _productos.Add(producto);
            return Ok(producto);
        }

        // GET /api/productos/{id}
        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id && p.Activo);
            if (producto == null)
                return NotFound("Producto no encontrado.");
            return Ok(producto);
        }

        // PUT /api/productos/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] Producto productoUpdate)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id && p.Activo);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            producto.Nombre = productoUpdate.Nombre;
            producto.Categoria = productoUpdate.Categoria;
            producto.Precio = productoUpdate.Precio;
            producto.Cantidad = productoUpdate.Cantidad;
            producto.StockMinimo = productoUpdate.StockMinimo;

            return Ok(producto);
        }

        // DELETE /api/productos/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id && p.Activo);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            producto.Activo = false;
            return Ok("Producto desactivado.");
        }

        // GET /api/productos/bajo-stock
        [HttpGet("bajo-stock")]
        public IActionResult ProductosBajoStock()
        {
            var bajos = _productos.Where(p => p.Cantidad < p.StockMinimo && p.Activo).ToList();
            return Ok(bajos);
        }

        // GET /api/productos
        [HttpGet]
        public IActionResult ListarProductos()
        {
            var activos = _productos.Where(p => p.Activo).ToList();
            return Ok(activos);
        }
    }
}
