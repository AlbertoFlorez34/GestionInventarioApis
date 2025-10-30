using Microsoft.AspNetCore.Mvc;
using ModuloMovimientos.Api.Models;
using System;

namespace ModuloMovimientos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        // Lista en memoria simulando una base de datos
        private static readonly List<Movimiento> _movimientos = new();
        private static int _nextId = 1;

        // POST /api/movimientos/entrada
        [HttpPost("entrada")]
        public IActionResult RegistrarEntrada([FromBody] Movimiento movimiento)
        {
            movimiento.Id = _nextId++;
            movimiento.Tipo = TipoMovimiento.Entrada;
            movimiento.Fecha = DateTime.Now;
            _movimientos.Add(movimiento);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = movimiento.Id }, movimiento);
        }

        // POST /api/movimientos/salida
        [HttpPost("salida")]
        public IActionResult RegistrarSalida([FromBody] Movimiento movimiento)
        {
            movimiento.Id = _nextId++;
            movimiento.Tipo = TipoMovimiento.Salida;
            movimiento.Fecha = DateTime.Now;
            _movimientos.Add(movimiento);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = movimiento.Id }, movimiento);
        }

        // GET /api/movimientos
        [HttpGet]
        public ActionResult<IEnumerable<Movimiento>> ObtenerTodos()
        {
            return Ok(_movimientos);
        }

        // GET /api/movimientos/{id}
        [HttpGet("{id}")]
        public ActionResult<Movimiento> ObtenerPorId(int id)
        {
            var movimiento = _movimientos.FirstOrDefault(m => m.Id == id);
            if (movimiento == null)
                return NotFound($"No se encontró el movimiento con Id {id}");
            return Ok(movimiento);
        }

        // GET /api/movimientos/por-fecha?desde=2025-01-01&hasta=2025-01-31
        [HttpGet("por-fecha")]
        public ActionResult<IEnumerable<Movimiento>> ObtenerPorFecha(DateTime desde, DateTime hasta)
        {
            var lista = _movimientos
                .Where(m => m.Fecha.Date >= desde.Date && m.Fecha.Date <= hasta.Date)
                .ToList();

            return Ok(lista);
        }
    }
}
