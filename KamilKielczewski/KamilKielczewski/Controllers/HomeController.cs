using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KamilKielczewski.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View("Index");
        }

        public ActionResult Form()
        {
            return View("Form");
        }

        public ActionResult Language()
        {
            return View("Language");
        }

        public ActionResult Change(string Language)
        {

            if (Language != null)
            {

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
            }

            HttpCookie myCookie = new HttpCookie("Language");
            myCookie.Value = Language;
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);

            return View("Index");
        }
    }
}