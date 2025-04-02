using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Concrate;

namespace MvcProjeKampi.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik

        Context c = new Context();
        
        public ActionResult Index()
        {
            var categoryCount = c.Categories.Count().ToString();
            ViewBag.CategoryCount = categoryCount;

            var heading = c.Headings.Count(l => l.CategoryID == 15).ToString();
            ViewBag.Heading = heading;

            var writer = c.Writers.Where(w => w.WriterName.Contains("a") || w.WriterName.Contains("A")).Count();
            ViewBag.Writer = writer;

            var headingMax = c.Headings.Where(u => u.CategoryID == c.Headings.GroupBy(x => x.CategoryID).OrderByDescending(x => x.Count())
               .Select(x => x.Key).FirstOrDefault()).Select(x => x.Category.CategoryName).FirstOrDefault();
            ViewBag.HeadingMax = headingMax;

            var trueStatus = c.Categories.Where(c => c.CategoryStatus == true).Count();
            var falseStatus = c.Categories.Where(c => c.CategoryStatus == false).Count();

            ViewBag.Status = (trueStatus - falseStatus);


            return View();
        }
    }
}