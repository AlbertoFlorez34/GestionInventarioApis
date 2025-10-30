using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Tests.Tests
{
    public class CategoriaTests
    {
        [Fact]
        public void CrearCategoria_AgregaCorrectamente()
        {
            // Arrange
            var controller = new CategoriaController();
            var categoria = new Categoria { Nombre = "Periféricos" };

            // Act
            var result = controller.CrearCategoria(categoria) as OkObjectResult;
            var categoriaCreada = result.Value as Categoria;

            // Assert
            Assert.NotNull(categoriaCreada);
            Assert.Equal("Periféricos", categoriaCreada.Nombre);
            Assert.Equal(1, categoriaCreada.Id);
        }

        [Fact]
        public void ListarCategorias_RetornaSoloActivas()
        {
            // Arrange
            var controller = new CategoriaController();
            controller.CrearCategoria(new Categoria { Nombre = "Periféricos" });
            controller.CrearCategoria(new Categoria { Nombre = "Accesorios" });
            controller.EliminarCategoria(1); // desactivamos la primera

            // Act
            var result = controller.ListarCategorias() as OkObjectResult;
            var categorias = result.Value as System.Collections.Generic.List<Categoria>;

            // Assert
            Assert.Single(categorias);
            Assert.Equal("Accesorios", categorias[0].Nombre);
        }

        [Fact]
        public void ActualizarCategoria_CambiaNombreCorrectamente()
        {
            // Arrange
            var controller = new CategoriaController();
            controller.CrearCategoria(new Categoria { Nombre = "Periféricos" });

            // Act
            var result = controller.ActualizarCategoria(1, new Categoria { Nombre = "Accesorios" }) as OkObjectResult;
            var categoriaActualizada = result.Value as Categoria;

            // Assert
            Assert.NotNull(categoriaActualizada);
            Assert.Equal("Accesorios", categoriaActualizada.Nombre);
        }

        [Fact]
        public void EliminarCategoria_DesactivaCategoria()
        {
            // Arrange
            var controller = new CategoriaController();
            controller.CrearCategoria(new Categoria { Nombre = "Periféricos" });

            // Act
            var result = controller.EliminarCategoria(1) as OkObjectResult;

            var listarResult = controller.ListarCategorias() as OkObjectResult;
            var categorias = listarResult.Value as System.Collections.Generic.List<Categoria>;

            // Assert
            Assert.Equal("Categoría desactivada.", result.Value);
            Assert.Empty(categorias); // ya no aparece en la lista
        }
    }
}
