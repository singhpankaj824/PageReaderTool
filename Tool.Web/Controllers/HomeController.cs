using System;
using System.Web.Mvc;
using Tool.Web.Models;
using Tool.Web.Repository;

namespace Tool.Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly IDocumentManager _documentManager;
        public HomeController(IDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DataModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            try
            {
                _documentManager.LoadDocument(form.URL);
                DocumentData documentData = new DocumentData
                {
                    ImageURL = _documentManager.GetAttributeValueOfElement("img", "src"),
                    TotalNumberOfWords = _documentManager.TotalNumberOfWord(),
                    TopOccurringWords = _documentManager.Search()
                };
                return View("_DocumentData", documentData);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("URL", $"{ex.Message}");
                return View(form);
            }



        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}