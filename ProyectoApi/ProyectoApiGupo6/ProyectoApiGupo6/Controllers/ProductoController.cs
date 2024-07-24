using ProyectoApiGupo6.Entidades;
using ProyectoApiGupo6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoApiGupo6.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("Producto/ConsultarProductos")]
        public ConfirmacionProducto ConsultarProductos(bool MostrarTodos) 
        {
            var respuesta = new ConfirmacionProducto();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarProductos(MostrarTodos).ToList();

                    if (datos.Count > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                        respuesta.Datos = datos;
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
        [HttpGet]
        [Route("Producto/ConsultarTiposCategorias")]
        public ConfirmacionTiposCategoria ConsultarTiposCategorias(bool MostrarTodos) 
        {
            var respuesta = new ConfirmacionTiposCategoria();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarTiposCategoria(MostrarTodos).ToList();

                    if (datos.Count > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                        respuesta.Datos = datos;
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

        [HttpPost]
        [Route("Producto/RegistrarProducto")]
        public Confirmacion RegistrarProducto(Producto producto)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.RegistrarProducto(producto.CategoriaId, producto.NombreProducto, producto.Descripcion, producto.Precio).FirstOrDefault();

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                        respuesta.ProductoId = resp.Value;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "La información del producto ya se encuentra registrada";
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
        [Route("Producto/ActualizarImagenProducto")]

        public Confirmacion ActualizarImagenProducto(Producto producto)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.ActualizarImagenProducto(producto.ProductoId, producto.RutaImagen);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "Se presentó un error al guardar la imagen";
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
        [Route("Producto/EliminarProducto")]
        public Confirmacion EliminarProducto(long ProductoId)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.EliminarProducto(ProductoId);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "El producto no se pudo eliminar";
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
        [Route("Producto/EliminarProductoPermanente")]
        public Confirmacion EliminarProductoPermante(long ProductoId)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.EliminarProductoPermanente(ProductoId);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "El producto no se pudo eliminar";
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
        [Route("Producto/ConsultarProducto")]
        public ConfirmacionProducto ConsultarProducto(long ProductoId)
        {
            var respuesta = new ConfirmacionProducto();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarProducto(ProductoId).FirstOrDefault();

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
        [Route("Producto/ActualizarProducto")]
        public Confirmacion ActualizarProducto(Producto producto)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.ActualizarProducto(producto.ProductoId, producto.CategoriaId, producto.NombreProducto, producto.Descripcion, producto.Precio, producto.Estado);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "El producto no se pudo actualizar";
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
