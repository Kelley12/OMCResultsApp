using System.Data.OleDb;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using OMCResultsApp.ViewModels;

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
            
            return View(GetRiders());
        }

        public IEnumerable<RiderViewModel> GetRiders()
        {
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                string sqlQuery = "Select racer_id,fname,lname,racing_nbr,city,state,sponsors FROM racer_info Order by lname";
                var command = new OleDbCommand(sqlQuery, conn);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return RiderViewModel.Create(reader);
                }

            }
        }

        public ActionResult Profile()
        {
            ViewBag.Title = "Rider Profile";

            return View();
        }
    }
}