//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoApiGupo6.Models
{
    using System;
    
    public partial class ConsultarProductos_Result
    {
        public long ProductoId { get; set; }
        public long categoriaId { get; set; }
        public string NombreProducto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string NombreCategoria { get; set; }
        public string rutaImagen { get; set; }
        public Nullable<bool> estado { get; set; }
    }
}
