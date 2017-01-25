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
            ViewBag.Message = "Select a series from the list below.";

            return View(GetSeries());
        }

        public IEnumerable<SeriesViewModel> GetSeries()
        {
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                string sqlQuery = "SELECT series_id,series_desc FROM series ORDER BY series_id DESC";
                var command = new OleDbCommand(sqlQuery, conn);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return SeriesViewModel.Create(reader);
                }
            }
        }

        public ActionResult Events()
        {
            ViewBag.Title = "Events";
            ViewBag.Message = "Select an event from the list below.";

            return View(GetEvents());
        }

        public IEnumerable<EventViewModel> GetEvents()
        {
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                string sqlQuery = "SELECT event_id, event_name,event_date FROM event_data ORDER BY event_date DESC";
                var command = new OleDbCommand(sqlQuery, conn);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return EventViewModel.Create(reader);
                }
            }
        }

        [HttpPost]
        public ActionResult Riders(string searchText = null)
        {
            ViewBag.Title = "Riders";
            ViewBag.Message = "Search for a rider using the box in the menu above.";


            return View(GetRiders(searchText));
        }

        public IEnumerable<RiderViewModel> GetRiders(string searchText)
        {
            string sqlQuery = null;
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                if (string.IsNullOrEmpty(searchText))
                {
                    sqlQuery = "Select racer_id,fname,lname,racing_nbr,city,state,sponsors FROM racer_info Order by lname";
                }
                else
                {
                    sqlQuery = "Select racer_id, fname, lname, racing_nbr, city, state, sponsors FROM racer_info WHERE fname like '%" + searchText + "%' or lname like '%" + searchText + "%' or fname + ' ' + lname like '%" + searchText + "%' or racing_nbr like '%" + searchText + "%' Order by lname";
                }
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