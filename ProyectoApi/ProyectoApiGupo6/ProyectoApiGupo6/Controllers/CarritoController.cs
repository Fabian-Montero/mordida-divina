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
    public class CarritoController : ApiController
    {
        [HttpPost]
        [Route("Carrito/AgregarCarrito")]
        public Confirmacion AgregarCarrito(Carrito entidad)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.AgregarCarrito(entidad.Usuarioid, entidad.Productoid, entidad.Cantidad);


                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "No se pudo agregar los datos al carrito";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Error de sistema";
            }

            return respuesta;
        }


        [HttpGet]
        [Route("Carrito/ConsultarCarrito")]
        public ConfirmacionCarrito ConsultarCarrito(long usuarioid)
        {
            var respuesta = new ConfirmacionCarrito();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {

                    var datos = db.ConsultarCarrito(usuarioid).ToList();
                    if (datos.Count > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                        respuesta.Datos = datos;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "Carrito Vacío";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.Codigo = -1;
                respuesta.Detalle = "Error de sistema";
            }

            return respuesta;
        }

        [HttpDelete]
        [Route("Carrito/EliminarCarrito")]
        public Confirmacion EliminarCarrito(long carritoid)
        {
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.EliminarCarrito(carritoid);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "El carrito no se pudo eliminar";
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
        [Route("Carrito/PagoCarrito")]
        public Confirmacion PagoCarrito(Carrito entidad) {
            
            var respuesta = new Confirmacion();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.PagoCarrito(entidad.Usuarioid);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "No se pudo realizar su pedido, intentelo nuevamente";
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
        [Route("Carrito/ConsultarPedidos")]
        public ConfirmacionCarrito ConsultarPedidos(long usuarioId)
        {
            var respuesta = new ConfirmacionCarrito();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarPedidos(usuarioId).ToList();

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
        [Route("Carrito/ConsultarPedidosMantenimiento")]
        public ConfirmacionCarrito ConsultarPedidosMantenimiento(bool MostrarTodos)
        {
            var respuesta = new ConfirmacionCarrito();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarPedidosMantenimiento(MostrarTodos).ToList();

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
        [Route("Carrito/ConsultarDetallePedidos")]
        public ConfirmacionCarrito ConsultarDetallePedidos(long maestroId)
        {
            var respuesta = new ConfirmacionCarrito();

            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var datos = db.ConsultarDetallePedidos(maestroId).ToList();

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

        [HttpDelete]
        [Route("Carrito/ActualizarEstadoPedido")]
        public Confirmacion ActualizarEstadoPedido(long maestroId)
        {
            var respuesta = new Confirmacion();
            try
            {
                using (var db = new MordidaDivinaEntities())
                {
                    var resp = db.ActualizarEstadoPedido(maestroId);

                    if (resp > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Detalle = string.Empty;
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Detalle = "No se logro actualizar el estado del pedido";
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

