using Microsoft.AspNetCore.Mvc;
using ModuloCategorias.Api.Models;

namespace ModuloCategorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private static List<Categoria> _categorias = new();
        private static int _nextId = 1;

        // POST /api/categorias
        [HttpPost]
        public IActionResult CrearCategoria([FromBody] Categoria categoria)
        {
            categoria.Id = _nextId++;
            _categorias.Add(categoria);
            return Ok(categoria);
        }

        // GET /api/categorias
        [HttpGet]
        public IActionResult ListarCategorias()
        {
            var activas = _categorias.Where(c => c.Activo).ToList();
            return Ok(activas);
        }

        // PUT /api/categorias/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarCategoria(int id, [FromBody] Categoria categoriaUpdate)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id && c.Activo);
            if (categoria == null)
                return NotFound("Categoría no encontrada.");

            categoria.Nombre = categoriaUpdate.Nombre;
            return Ok(categoria);
        }

        // DELETE /api/categorias/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id && c.Activo);
            if (categoria == null)
                return NotFound("Categoría no encontrada.");

            categoria.Activo = false;
            return Ok("Categoría desactivada.");
        }
    }
}
