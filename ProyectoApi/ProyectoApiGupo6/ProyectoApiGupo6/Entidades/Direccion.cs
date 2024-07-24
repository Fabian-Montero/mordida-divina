using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoApiGupo6.Entidades
{
    public class Direccion
    {

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }
    }
}