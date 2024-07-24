using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebGrupo6.Entidades
{
    public class Producto
    {
        public long ProductoId { get; set; }

        public bool Estado { get; set; }
        public long CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public string RutaImagen { get; set; }

        



    }

     public class ConfirmacionProducto
     {
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public List<Producto> Datos { get; set; }
        public Producto Dato { get; set; }
     }
}