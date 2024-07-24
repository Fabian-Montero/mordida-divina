using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWebGrupo6.Entidades
{
    public class Usuario
    {
        public Direccion Direccion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Contrasenna { get; set; }
        public string NuevaContrasenna { get; set; }
        public string ConfirmacionContrasenna { get; set; }
        public bool Activo { get; set; }
        public bool Temporal{ get; set; }

        public long Id {get; set;}

        public long rolId { get; set; }
        public string NombreRol { get; set; }

        public DateTime? Vencimiento { get; set; }

        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }

    }

    public class ConfirmacionUsuario
    { 
        public int Codigo { get; set; }
        public String Detalle { get; set; }
        public Usuario Usuario { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public  Usuario Dato { get; set; }
        public List<Usuario> Datos { get; set; }



    }
}