using ProyectoWebGrupo6.Entidades;
using ProyectoWebGrupo6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace ProyectoWebGrupo6.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class UsuarioController : Controller
    {
        UsuarioModel usuarioModel = new UsuarioModel();
        CarritoModel carritoModel = new CarritoModel();

        [HttpGet]
        public ActionResult IniciarSesionUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesionUsuario(Usuario usuario)
        {

            var respuesta = usuarioModel.IniciarSesionUsuario(usuario);

            if (respuesta.Codigo == 0)
            {
                string nombreCompleto = respuesta.Usuario.Nombre + " " + respuesta.Usuario.Apellidos;
                Session["NombreCompleto"] = nombreCompleto;
                Session["RolUsuario"] = respuesta.Usuario.rolId;
                Session["NombreRol"] = respuesta.Usuario.NombreRol;
                Session["UsuarioId"] = respuesta.Usuario.Id;

                var datos = carritoModel.ConsultarCarrito(long.Parse(Session["UsuarioId"].ToString()));

                if (datos.Codigo == 0)
                {
                    Session["Cantidad"] = datos.Datos.AsEnumerable().Sum(x => x.Cantidad);
                    Session["SubTotal"] = datos.Datos.AsEnumerable().Sum(x => x.SubTotal);
                    Session["Total"] = datos.Datos.AsEnumerable().Sum(x => x.Total);
                }
                else
                {
                    Session["Cantidad"] = "0";
                    Session["SubTotal"] = "0";
                    Session["Total"] = "0";
                }

                return RedirectToAction("ConsultarProducto", "Producto");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult RegistrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            var respuesta = usuarioModel.RegistrarUsuario(usuario);

            if (usuario.Contrasenna != usuario.ConfirmacionContrasenna)
            {
                ViewBag.msjPantalla = "Error al registrar usuario. Las contraseñas deben de ser iguales";
                return View();
            }
            else if (respuesta.Codigo == 0)
            {
                return RedirectToAction("IniciarSesionUsuario", "Usuario");
            }
            else
            {
                ViewBag.msjPantalla = respuesta.Detalle;
                return View();
            }
            
        }

        public ActionResult Error401()
        {
            return View();
        }
        public ActionResult Error401Login()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EnvioCodigoAcceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnvioCodigoAcceso(Usuario usuario)
        {
            var respuesta = usuarioModel.EnvioCodigoAcceso(usuario);
            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("RegistrarNuevaContrasenna", "Usuario");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult RegistrarNuevaContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarNuevaContrasenna(Usuario usuario)
        {
            var respuesta = usuarioModel.RegistrarNuevaContrasenna(usuario);
            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("IniciarSesionUsuario", "Usuario");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult CerrarSesionUsuario()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesionUsuario","Usuario");
        }

        //CRUD USUARIOS

        [HttpGet]
        [FiltroSeguridad]
        [FiltroAdmin]
        public ActionResult MantenimientoUsuarios()
        {
            var  respuesta = usuarioModel.ConsultarUsuarios();
            if (respuesta.Codigo ==0)
            {
                return View(respuesta.Datos);
            }
            else {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Producto>());
            }
            
        }

        [HttpGet]
        [FiltroAdmin]
        [FiltroEditarUsuario]
        public ActionResult ActualizarUsuarioMantenimiento(long id)
        {
            var respuesta = usuarioModel.ConsultarUsuarioMantenimiento(id);
            CargarViewBagRoles();
            CargarViewBagEstado();
            CargarViewBagTemporal();
            
            return View(respuesta.Dato);
            
        }
        [FiltroAdmin]
        [HttpPost]
        public ActionResult ActualizarUsuarioMantenimiento(Usuario usuario)
        {
            var respuesta = usuarioModel.ActualizarUsuarioMantenimiento(usuario);
            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoUsuarios", "Usuario");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public ActionResult EliminarUsuarioMantenimiento(long id)
        {
            var respuesta = usuarioModel.EliminarUsuarioMantenimiento(id);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoUsuarios", "Usuario");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [FiltroSeguridad]
        [HttpGet]
        public ActionResult ModificarUsuario()
        {
            var resp = usuarioModel.ConsultarUsuario();
            return View(resp.Usuario);
        }
    
        [HttpPost]
        public ActionResult ModificarUsuario(Usuario entidad)
        {
            var respuesta = usuarioModel.ModificarUsuario(entidad);

            if (respuesta.Codigo == 0)
            {
                ViewBag.MsjPantallaExito = respuesta.Detalle;
                string nombreCompleto = respuesta.Usuario.Nombre + " " + respuesta.Usuario.Apellidos;
                Session["NombreCompleto"] = nombreCompleto;
                return View();
            }

            else
            {
                ViewBag.MsjPantallaFallida = respuesta.Detalle;
                return View();
            }
        }

        private void CargarViewBagEstado()
        {

            var tiposEstado = new List<SelectListItem>();

            tiposEstado.Add(new SelectListItem { Text = "Seleccione un estado", Value = "" });
            tiposEstado.Add(new SelectListItem { Text = "Activo", Value = true.ToString() });
            tiposEstado.Add(new SelectListItem { Text = "Inactivo", Value = false.ToString() });

            ViewBag.TiposEstado = tiposEstado;
        }

        private void CargarViewBagTemporal()
        {

            var tiposTemporal = new List<SelectListItem>();

            tiposTemporal.Add(new SelectListItem { Text = "Seleccione si desea que el usuario tenga una contraseña temporal", Value = "" });
            tiposTemporal.Add(new SelectListItem { Text = "Sí", Value = true.ToString() });
            tiposTemporal.Add(new SelectListItem { Text = "No", Value = false.ToString() });

            ViewBag.TiposTemporal = tiposTemporal;
        }

        private void CargarViewBagRoles()
        {
            var respuesta =  usuarioModel.ConsultarTiposRoles();
            var tiposRoles = new List<SelectListItem>();

            tiposRoles.Add(new SelectListItem { Text = "Seleccione un rol", Value = "" });
            foreach (var role in respuesta.Datos)
                tiposRoles.Add(new SelectListItem { Text = role.NombreRol, Value = role.RolId.ToString() });

            ViewBag.TiposRoles = tiposRoles;
        }
    }
}