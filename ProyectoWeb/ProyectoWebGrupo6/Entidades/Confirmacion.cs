using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebGrupo6.Entidades
{
    public class Confirmacion
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }

        public long ProductoId { get; set; }


    }

    public class ConfirmacionImagen
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }

        public string RutaImagen { get; set; }

    }
}