using ProyectoWebGrupo6.Entidades;
using ProyectoWebGrupo6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWebGrupo6.Controllers
{
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class CarritoController : Controller
    {
        CarritoModel Carritomodel = new CarritoModel();

        [HttpPost]
        public ActionResult AgregarCarrito(long idProducto, int cantProducto) 
        {
            Carrito entidad = new Carrito();
            entidad.Usuarioid = long.Parse(Session["UsuarioId"].ToString());
            entidad.Productoid = idProducto;
            entidad.Cantidad = cantProducto;

            var respuesta = Carritomodel.AgregarCarrito(entidad);

            if (respuesta.Codigo == 0)
            {
                ActualizarCarrito();
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ConsultaCarrito()
        {
            var respuesta = Carritomodel.ConsultarCarrito(long.Parse(Session["UsuarioId"].ToString()));

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Carrito>());
            }
        }

        [HttpPost]
        public ActionResult EliminarCarrito(Carrito entidad)
        {
            var respuesta = Carritomodel.EliminarCarrito(entidad.Carritoid);
            
            if (respuesta.Codigo == 0)
            {
                ActualizarCarrito();
                return RedirectToAction("ConsultaCarrito", "Carrito");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpPost]
        public ActionResult PagoCarrito(Carrito entidad)
        {
            entidad.Usuarioid = long.Parse(Session["UsuarioId"].ToString());
            var respuesta = Carritomodel.PagoCarrito(entidad);

            if (respuesta.Codigo == 0)
            {
                ActualizarCarrito();
                return RedirectToAction("ConsultarProducto", "Producto");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;

                var items = Carritomodel.ConsultarCarrito(long.Parse(Session["UsuarioId"].ToString()));
                return View("ConsultaCarrito", items.Datos);
            }
        }

        [HttpGet]
        public ActionResult ConsultarPedidos()
        {
            var respuesta = Carritomodel.ConsultarPedidos(long.Parse(Session["UsuarioId"].ToString()));

            if (respuesta.Codigo == 0)
            {
                return View(respuesta.Datos);
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Carrito>());
            }
        }
       
        [HttpGet]
        [FiltroAdmin]
        public ActionResult PedidosMantenimiento()
        {
            var respuesta = Carritomodel.ConsultarPedidosMantenimiento(true);

            if (respuesta.Codigo == 0)
            {
                return View(respuesta.Datos);
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Carrito>());
            }
        }

        [HttpGet]
        public ActionResult ConsultarDetalleFacturas(long id)
        {
            var respuesta = Carritomodel.ConsultarDetallePedidos(id);

            if (respuesta.Codigo == 0)
            {
                return View(respuesta.Datos);
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Carrito>());
            }
        }

        [HttpGet]
        public ActionResult ActualizarEstadoPedido(long id)
        {
            var respuesta = Carritomodel.ActualizarEstadoPedido(id);
            if(respuesta.Codigo == 0)
            {
                return RedirectToAction("PedidosMantenimiento", "Carrito");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        private void ActualizarCarrito()
        {
            var datos = Carritomodel.ConsultarCarrito(long.Parse(Session["UsuarioId"].ToString()));

            if (datos.Codigo == 0)
            {
                Session["Cantidad"] = datos.Datos.AsEnumerable().Sum(x => x.Cantidad);
                Session["SubTotal"] = datos.Datos.AsEnumerable().Sum(x => x.SubTotal);
                Session["Total"] = datos.Datos.AsEnumerable().Sum(x => x.Total);
            }
            
            else
            {
                Session["Cantidad"] = 0;
                Session["SubTotal"] = 0;
                Session["Total"] = 0;
            }
            
        }
    }
}