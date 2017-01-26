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

        public ActionResult Classes(int? id)
        {
            ViewBag.Title = "Classes";
            ViewBag.Message = "";

            if(id == null)
            {
                return HttpNotFound();
            }

            return View(GetClasses(id));
        }

        public IEnumerable<ClassViewModel> GetClasses(int? EventId)
        {
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                string sqlQuery = "SELECT event_id, [class].class_id, class_desc FROM (event_classes INNER JOIN [class] ON [event_classes].class_id = [class].class_id) WHERE event_id = " + EventId;
                var command = new OleDbCommand(sqlQuery, conn);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return ClassViewModel.Create(reader);
                }
            }
        }

        public ActionResult Results(int EventId, int ClassId)
        {
            ViewBag.Title = "Results";
            ViewBag.Message = "";
            
            return View(GetResults(EventId, ClassId));
        }

        public IEnumerable<ResultsViewModel> GetResults(int EventId, int ClassId)
        {
            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();

                string sqlQuery = "SELECT [event_data].event_id, [event_data].event_name, [event_data].event_date, [class].class_id, [class].class_desc, [racer_info].racer_id, fname + ' ' + lname AS Name, city + ', ' + state AS Location, [event_entries].moto_1_finish, [event_entries].moto_2_finish, [event_entries].overall_finish ";
                sqlQuery += "FROM(([event_data] INNER JOIN[event_entries] ON [event_data].event_id = [event_entries].event_id) INNER JOIN [racer_info] ON [event_entries].racer_id = [racer_info].racer_id) ";
                sqlQuery += "INNER JOIN[class] ON [event_entries].class_id = [class].class_id WHERE [class].class_id = " + ClassId + " AND [event_data].event_id = " + EventId + " ORDER BY [event_entries].overall_finish";

                var command = new OleDbCommand(sqlQuery, conn);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return ResultsViewModel.Create(reader);
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