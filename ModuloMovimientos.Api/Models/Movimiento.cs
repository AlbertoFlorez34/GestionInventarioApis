namespace ModuloMovimientos.Api.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; } = string.Empty;
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public TipoMovimiento Tipo { get; set; }
    }
}
