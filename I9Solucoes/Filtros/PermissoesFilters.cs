using System.Web.Mvc;
using System.Web;

namespace I9Solucoes.Filtro
{
    public class PermissoesFilters : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Request.Cookies["login"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Login");
            }
        }
    }
}