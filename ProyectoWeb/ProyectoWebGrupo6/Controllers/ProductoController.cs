using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using ProyectoWebGrupo6.Entidades;
using ProyectoWebGrupo6.Models;

namespace ProyectoWebGrupo6.Controllers
{
    [FiltroSeguridad]
    [OutputCache(NoStore = true, Duration = 0)]

    public class ProductoController : Controller
    {

        ProductoModel productoModel = new ProductoModel();
        CarritoModel carritoModel = new CarritoModel();

        [HttpGet]
        public ActionResult ConsultarProducto()
        {
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

            var respuesta = productoModel.ConsultarProductos(false);

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
        [FiltroAdmin]
        public ActionResult MantenimientoProductos() {

            var respuesta = productoModel.ConsultarProductos(true);

            if (respuesta.Codigo == 0) {

                //List<Producto> productosOrdenados = respuesta.Datos.OrderByDescending(p => p.Estado).ToList();
                return View(respuesta.Datos);
            } else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Producto>());
            }

        }

        [HttpGet]
        [FiltroAdmin]
        public ActionResult RegistrarProducto()
        {
            ConsultarTiposCategorias();
            return View();
        }

        [HttpPost]
        [FiltroAdmin]
        public async Task<ActionResult> RegistrarProducto(HttpPostedFileBase ImagenProducto, Producto producto)
        {
            //Registra el usuario y retorna el id
            var respuesta = productoModel.RegistrarProducto(producto);

            if (respuesta.Codigo == 0)
            {

                // sube la imagen a supabase        
                var respuestaImagen = await SubirImagen(ImagenProducto, respuesta.ProductoId);

                if (respuestaImagen.Codigo == 0)
                {
                    //actualiza la ruta de la imagen en la base de datos sql
                    producto.RutaImagen = respuestaImagen.RutaImagen;
                    producto.ProductoId = respuesta.ProductoId;
                    productoModel.ActualizarImagenProducto(producto);

                    return RedirectToAction("MantenimientoProductos", "Producto");
                }
                else
                {
                    //Eliminar producto creado
                    EliminarProductoPermanente(respuesta.ProductoId);

                    //Mensaje de error cuando no es formato PNG
                    ViewBag.MsjPantalla = respuestaImagen.Detalle;
                    ConsultarTiposCategorias();
                    return View();
                }
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                ConsultarTiposCategorias();
                return View();
            }
        }
        [HttpGet]
        [FiltroAdmin]
        public async Task<ActionResult> EliminarProducto(long ProductoId)
        {
            var respuesta = productoModel.EliminarProducto(ProductoId);
            //await EliminarImagen(ProductoId);
            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoProductos", "Producto");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<ActionResult> EliminarProductoPermanente(long ProductoId)
        {
            var respuesta = productoModel.EliminarProductoPermanente(ProductoId);
            await EliminarImagen(ProductoId);
            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("MantenimientoProductos", "Producto");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public ActionResult ActualizarProducto(long ProductoId)
        {
            var resp = productoModel.ConsultarProducto(ProductoId);
            ConsultarTiposCategorias();
            CargarViewBagEstado();
            ViewBag.urlImagen = resp.Dato.RutaImagen;
            return View(resp.Dato);
        }

        [HttpPost]
        public async Task<ActionResult> ActualizarProducto(HttpPostedFileBase ImagenProducto, Producto producto)
        {
            
                if (ImagenProducto != null)
                {
                    //Eliminar la imagen previa
                    await EliminarImagen(producto.ProductoId);

                    //Guardar nueva imagen
                    var RespuestaImagen = await SubirImagen(ImagenProducto, producto.ProductoId);
                    

                    if (RespuestaImagen.Codigo == 0)
                    {
                        //actualiza la ruta de la imagen en la base de datos sql
                        
                        var RutaImagen = await ObtenerImagen(producto.ProductoId);
                        producto.RutaImagen = RutaImagen.RutaImagen;
                        productoModel.ActualizarImagenProducto(producto);

                    }
                    else
                    {
                        //Mensaje de error cuando no es formato PNG
                        ViewBag.MsjPantalla = RespuestaImagen.Detalle;
                        ConsultarTiposCategorias();
                        CargarViewBagEstado();
                        return View();
                    }
                }

                //actualizar la info restante del usuario
                var respuesta = productoModel.ActualizarProducto(producto);

                if (respuesta.Codigo == 0)
                {
                    return RedirectToAction("MantenimientoProductos", "Producto");
                }
                else
                {
                    ConsultarTiposCategorias();
                    ViewBag.MsjPantalla = respuesta.Detalle;
                    return View();
                }
        }

        private void ConsultarTiposCategorias()
        {
            var respuesta = productoModel.ConsultarTiposCategorias(false);
            var tiposCategoria = new List<SelectListItem>();

            tiposCategoria.Add(new SelectListItem { Text = "Seleccione una categoría", Value = "" });
            foreach (var item in respuesta.Datos)
                tiposCategoria.Add(new SelectListItem { Text = item.NombreCategoria, Value = item.CategoriaId.ToString() });

            ViewBag.TiposCategoria = tiposCategoria;
        }

        private void CargarViewBagEstado()
        {

            var tiposEstado = new List<SelectListItem>();

            tiposEstado.Add(new SelectListItem { Text = "Seleccione un estado", Value = "" });
            tiposEstado.Add(new SelectListItem { Text = "Activo", Value = true.ToString() });
            tiposEstado.Add(new SelectListItem { Text = "Inactivo", Value = false.ToString() });

            ViewBag.TiposEstado = tiposEstado;
        }


        //Supabase 

        public async Task<Supabase.Client> Supabase()

        {
            string urlProyecto = "https://micnqvjrnzhesjgnogsc.supabase.co";
            string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1pY25xdmpybnpoZXNqZ25vZ3NjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTEzODM0MjEsImV4cCI6MjAyNjk1OTQyMX0.jvTYmDVu_g7LT_wbm32DxgWAmGGljwT69V-opGL3_cs";
            var opciones = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(urlProyecto, apiKey, opciones);
            await supabase.InitializeAsync();

            return supabase;

        }

        public async Task<ConfirmacionImagen> SubirImagen(HttpPostedFileBase ImagenProducto, long productoId)
        {

            var respuestaImagen = new ConfirmacionImagen();

            try
            {
                var supabase = await Supabase();



                var memoryStream = new MemoryStream();

                await ImagenProducto.InputStream.CopyToAsync(memoryStream);

                byte[] imagenBytes = memoryStream.ToArray();

                var index = ImagenProducto.FileName.LastIndexOf('.');

                string extension = ImagenProducto.FileName.Substring(index + 1);



                await supabase.Storage
                  .From("imagenes-producto")
                  .Upload(imagenBytes, $"producto-{productoId}.{extension}");

                respuestaImagen.Codigo = 0;
                respuestaImagen.Detalle = "";
                respuestaImagen.RutaImagen = supabase.Storage.From("imagenes-producto").GetPublicUrl($"producto-{productoId}.png?timestamp={DateTime.Now.Ticks}");


                return respuestaImagen;
            }
            catch (Exception ex)
            {
                respuestaImagen.Codigo = -1;
                respuestaImagen.Detalle = "Ya existe este producto o se utilizó un formato de imagen no válido, recuerde usar el formato de imagen PNG";
                respuestaImagen.RutaImagen = "";
                return respuestaImagen;
            }

        }

        public async Task<ConfirmacionImagen> ObtenerImagen(long ProductoId)
        {
            var respuestaImagen = new ConfirmacionImagen();
            try
            {
                var supabase = await Supabase();

                 string urlImagen = supabase.Storage
                    .From("imagenes-producto")
                    .GetPublicUrl($"producto-{ProductoId}.png?timestamp={DateTime.Now.Ticks}");

                respuestaImagen.Codigo = 0;
                respuestaImagen.Detalle = "";
                respuestaImagen.RutaImagen = urlImagen;
                return respuestaImagen;
            }
            catch (Exception ex)
            {
                respuestaImagen.Codigo = -1;
                respuestaImagen.Detalle = "Error a la hora de obtener la imagen";
                respuestaImagen.RutaImagen = "";
                return respuestaImagen;
            }
        }
        public async Task<Object> EliminarImagen(long ProductoId)
        {
            try
            {
                var supabase = await Supabase();

                await supabase.Storage
                    .From("imagenes-producto")
                    .Remove(new List<string> { $"producto-{ProductoId}.png" });
                return "Imagen eliminada";
            }
            catch (Exception ex)
            {
                return ex;
            }
        }


    }
}