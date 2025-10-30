namespace MóduloProductos.Api.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; } = true;
    }
}
