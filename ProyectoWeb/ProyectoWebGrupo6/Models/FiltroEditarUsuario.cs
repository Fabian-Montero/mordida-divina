using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoWebGrupo6.Models
{
    public class FiltroEditarUsuario : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UsuarioId"].ToString() == filterContext.ActionParameters["id"].ToString())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Usuario" },
                    { "action", "Error401"} 
                });
            }
        }
    }
}