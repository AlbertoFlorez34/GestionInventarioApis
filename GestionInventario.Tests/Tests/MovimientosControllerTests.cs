using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Tests.Tests
{
    public class MovimientosControllerTests
    {
        private readonly MovimientosController _controller;

        public MovimientosControllerTests()
        {
            _controller = new MovimientosController();
        }

        [Fact]
        public void RegistrarEntrada_DeberiaCrearMovimientoEntrada()
        {
            // Arrange
            var nuevoMovimiento = new Movimiento
            {
                ProductoId = 1,
                Cantidad = 10,
                Descripcion = "Compra inicial"
            };

            // Act
            var resultado = _controller.RegistrarEntrada(nuevoMovimiento) as CreatedAtActionResult;
            var movimiento = resultado?.Value as Movimiento;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(201, resultado.StatusCode);
            Assert.NotNull(movimiento);
            Assert.Equal(TipoMovimiento.Entrada, movimiento.Tipo);
            Assert.Equal(10, movimiento.Cantidad);
        }

        [Fact]
        public void RegistrarSalida_DeberiaCrearMovimientoSalida()
        {
            // Arrange
            var nuevoMovimiento = new Movimiento
            {
                ProductoId = 2,
                Cantidad = 5,
                Descripcion = "Venta de producto"
            };

            // Act
            var resultado = _controller.RegistrarSalida(nuevoMovimiento) as CreatedAtActionResult;
            var movimiento = resultado?.Value as Movimiento;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(201, resultado.StatusCode);
            Assert.NotNull(movimiento);
            Assert.Equal(TipoMovimiento.Salida, movimiento.Tipo);
        }

        [Fact]
        public void ObtenerTodos_DeberiaRetornarLista()
        {
            // Arrange
            _controller.RegistrarEntrada(new Movimiento { ProductoId = 1, Cantidad = 10 });
            _controller.RegistrarSalida(new Movimiento { ProductoId = 2, Cantidad = 5 });

            // Act
            var resultado = _controller.ObtenerTodos() as OkObjectResult;
            var lista = resultado?.Value as List<Movimiento>;

            // Assert
            Assert.NotNull(resultado);
            Assert.NotEmpty(lista);
            Assert.True(lista.Count >= 2);
        }

        [Fact]
        public void ObtenerPorId_CuandoNoExiste_DeberiaRetornarNotFound()
        {
            // Act
            var resultado = _controller.ObtenerPorId(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        [Fact]
        public void ObtenerPorFecha_DeberiaFiltrarCorrectamente()
        {
            // Arrange
            _controller.RegistrarEntrada(new Movimiento { ProductoId = 3, Cantidad = 5, Fecha = new DateTime(2025, 10, 1) });
            _controller.RegistrarSalida(new Movimiento { ProductoId = 4, Cantidad = 2, Fecha = new DateTime(2025, 10, 15) });

            var desde = new DateTime(2025, 10, 1);
            var hasta = new DateTime(2025, 10, 10);

            // Act
            var resultado = _controller.ObtenerPorFecha(desde, hasta) as OkObjectResult;
            var lista = resultado?.Value as List<Movimiento>;

            // Assert
            Assert.NotNull(lista);
            Assert.Single(lista);
        }
    }
}
