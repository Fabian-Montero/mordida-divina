using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebGrupo6.Entidades
{
    public class Carrito
    {
        public long Carritoid { get; set; }
        public long Usuarioid { get; set; }
        public long Productoid { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Precio { get; set; }
        public string Nombre { get; set; }
        public long MaestroId { get; set; }
        public long DetalleId { get; set; }
        public bool Estado { get; set; }
    }

    public class ConfirmacionCarrito
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }

        public List<Carrito> Datos { get; set; }

        public Carrito Dato { get; set; }
    }
}