using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using ProyectoWebGrupo6.Entidades;

namespace ProyectoWebGrupo6.Models
{
    public class CategoriaModel
    {
        public string url = ConfigurationManager.AppSettings["urlWebApi"];

        public ConfirmacionTiposCategoria ConsultarTiposCategoria(bool MostrarTodos)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Categoria/ConsultarCategorias?MostrarTodos=" + MostrarTodos;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionTiposCategoria>().Result;
                else
                    return null;
            }
        }

        public ConfirmacionTiposCategoria RegistrarCategoria(TiposCategorias categoria)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Categoria/RegistrarCategoria";
                JsonContent jsonEntidad = JsonContent.Create(categoria);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;
                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionTiposCategoria>().Result;
                else
                    return null;
            }
        }

        public ConfirmacionTiposCategoria ConsultarCategoria(long CategoriaId)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Categoria/ConsultarCategoria?CategoriaId=" + CategoriaId;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionTiposCategoria>().Result;
                else
                    return null;
            }
        }

        public ConfirmacionTiposCategoria ActualizarCategoria(TiposCategorias categoria)
        {
            using (var client = new HttpClient())
            {

                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Categoria/ActualizarCategoria?CategoriaId=" + categoria.CategoriaId;
                JsonContent jsonEntidad = JsonContent.Create(categoria);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionTiposCategoria>().Result;
                else
                    return null;
            }
        }

        public ConfirmacionTiposCategoria EliminarCategoria(long CategoriaId)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Categoria/EliminarCategoria?CategoriaId=" + CategoriaId;
                var respuesta = client.DeleteAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ConfirmacionTiposCategoria>().Result;
                else
                    return null;
            }
        }
    }
}