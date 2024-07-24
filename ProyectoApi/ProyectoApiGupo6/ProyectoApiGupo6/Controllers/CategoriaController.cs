using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoApiGupo6.Entidades;
using ProyectoApiGupo6.Models;

namespace ProyectoApiGupo6.Controllers
{
    public class CategoriaController : ApiController
    {

        [HttpGet]
        [Route("Categoria/ConsultarCategorias")]
        public ConfirmacionTiposCategoria ConsultarTiposCategoria(bool MostrarTodos)
        {
            var respuesta = new ConfirmacionTiposCategoria();
            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarTiposCategoria(MostrarTodos).ToList();

                    if (datos != null)

                    {

                        respuesta.Codigo = 0;

                        respuesta.Detalle = string.Empty;

                        respuesta.Datos = datos;

                    }

                    else

                    {

                        respuesta.Codigo = -1;

                        respuesta.Detalle = "No se pudo encontrar información de las categorías";

                    }

                }

            }

            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";

            }
            return respuesta;
        }

        [HttpPost]
        [Route("Categoria/RegistrarCategoria")]
        public ConfirmacionTiposCategoria RegistrarCategoria(TiposCategorias categoria)
        {
            var respuesta = new ConfirmacionTiposCategoria();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.RegistrarCategoria(categoria.NombreCategoria).FirstOrDefault();

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "La información de la categoría ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";
            }

            return respuesta;
        }

        [HttpGet]
        [Route("Categoria/ConsultarCategoria")]
        public ConfirmacionTiposCategoria ConsultarCategoria(long CategoriaId)
        {
            var respuesta = new ConfirmacionTiposCategoria();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarCategoria(CategoriaId).FirstOrDefault();

                    if (datos != null)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                        respuesta.Dato = datos;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "No se encontraron resultados";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";
            }

            return respuesta;
        }

        [HttpPut]
        [Route("Categoria/ActualizarCategoria")]
        public ConfirmacionTiposCategoria ActualizarCategoria(TiposCategorias categoria)
        {
            var respuesta = new ConfirmacionTiposCategoria();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.ActualizarCategoria(categoria.CategoriaId,categoria.NombreCategoria, categoria.Estado);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "Se presentó un error al actualizar la categoría";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";
            }

            return respuesta;
        }

        [HttpDelete]
        [Route("Categoria/EliminarCategoria")]
        public ConfirmacionTiposCategoria EliminarCategoria(long CategoriaId)
        {
            var respuesta = new ConfirmacionTiposCategoria();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.EliminarCategoria(CategoriaId);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "La categoría no se pudo eliminar";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Se presentó un error en el sistema";
            }

            return respuesta;
        }

    }
}
