using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Tests.Tests
{
    public class ProductoTests
    {
        [Fact]
        public void CrearProducto_AgregaProductoCorrectamente()
        {
            // Arrange
            var controller = new ProductoController();
            var producto = new Producto
            {
                Nombre = "Teclado",
                Categoria = "Periféricos",
                Precio = 200,
                Cantidad = 10,
                StockMinimo = 5
            };

            // Act
            var result = controller.CrearProducto(producto) as OkObjectResult;
            var productoCreado = result.Value as Producto;

            // Assert
            Assert.NotNull(productoCreado);
            Assert.Equal("Teclado", productoCreado.Nombre);
            Assert.Equal(1, productoCreado.Id);
        }

        [Fact]
        public void ObtenerProducto_RetornaProductoExistente()
        {
            // Arrange
            var controller = new ProductoController();
            var producto = new Producto
            {
                Nombre = "Mouse",
                Categoria = "Periféricos",
                Precio = 120,
                Cantidad = 15,
                StockMinimo = 5
            };
            controller.CrearProducto(producto);

            // Act
            var result = controller.ObtenerProducto(1) as OkObjectResult;
            var productoObtenido = result.Value as Producto;

            // Assert
            Assert.NotNull(productoObtenido);
            Assert.Equal("Mouse", productoObtenido.Nombre);
        }
    }
}
