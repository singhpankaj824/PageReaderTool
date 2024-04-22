using System.Web.Mvc;

namespace Tool.Web.Controllers
{
    public class HttpErrorsController : Controller
    {
        // GET: HttpErrors
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }
        public ActionResult Error()
        {
            Response.StatusCode = 500;

            return View();
        }
    }
}