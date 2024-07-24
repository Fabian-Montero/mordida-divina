using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebGrupo6.Entidades
{
    public class TiposCategorias
    {
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }

        public bool Estado { get; set; }    
    }

    public class ConfirmacionTiposCategoria
    {
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public List<TiposCategorias> Datos { get; set; }
        public TiposCategorias Dato { get; set; }
    }
}