using System.Data.OleDb;
using System.Web.Mvc;
using System.Configuration;

namespace OMCResultsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["TracksideConnection"].ConnectionString;

        public ActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }

        public ActionResult Series()
        {
            ViewBag.Title = "Series";

            return View();
        }

        public ActionResult Events()
        {
            ViewBag.Title = "Events";

            return View();
        }

        public ActionResult Riders()
        {
            ViewBag.Title = "Riders";

            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                var sqlQuery = "Select * FROM racer_info";
                var command = new OleDbCommand(sqlQuery, conn);
                command.ExecuteNonQuery();
            }
            return View();
        }

        public ActionResult Profile()
        {
            ViewBag.Title = "Rider Profile";

            return View();
        }
    }
}