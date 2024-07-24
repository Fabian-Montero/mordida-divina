using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebGrupo6.Entidades
{
    public class TiposRoles
    {
        public int RolId { get; set; }
        public string NombreRol { get; set; }
    }

    public class ConfirmacionTiposRoles
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public List<TiposRoles> Datos { get; set; }
        public TiposRoles Dato { get; set; }
    }
}