using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProyectoWebGrupo6.Entidades;
using ProyectoWebGrupo6.Models;

namespace ProyectoWebGrupo6.Controllers
{
    public class CategoriaController : Controller
    {
        //CRUD Categorías

        CategoriaModel categoriaModel = new CategoriaModel();

        [HttpGet]
        [FiltroSeguridad]
        [FiltroAdmin]
        public ActionResult MantenimientoCategorias()
        {
            var respuesta = categoriaModel.ConsultarTiposCategoria(true);

            if (respuesta.Codigo == 0)
            {
                return View(respuesta.Datos);
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Producto>());
            }

        }

        [HttpGet]
        [FiltroSeguridad]
        [FiltroAdmin]
        public ActionResult RegistrarCategoria()
        {
            return View();
        }

        [HttpPost]
        [FiltroSeguridad]
        [FiltroAdmin]
        public ActionResult RegistrarCategoria(TiposCategorias categorias)
        {
            var respuesta = categoriaModel.RegistrarCategoria(categorias);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoCategorias","Categoria");
            }
            else
            {
                ViewBag.msjPantalla = respuesta.Detalle;
                return View();
            }

        }

        [HttpGet]
        [FiltroSeguridad]
        [FiltroAdmin]
        public ActionResult ActualizarCategoria(long CategoriaId)
        {
            var respuesta = categoriaModel.ConsultarCategoria(CategoriaId);
            CargarViewBagEstado();
            return View(respuesta.Dato);
        }

        [FiltroAdmin]
        [HttpPost]
        public ActionResult ActualizarCategoria(TiposCategorias categoria)
        {
            var respuesta = categoriaModel.ActualizarCategoria(categoria);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoCategorias", "Categoria");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public ActionResult EliminarCategoria(long CategoriaId)
        {
            var respuesta = categoriaModel.EliminarCategoria(CategoriaId);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoCategorias","Categoria");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return RedirectToAction("MantenimientoCategorias", "Categoria");
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

    }
}