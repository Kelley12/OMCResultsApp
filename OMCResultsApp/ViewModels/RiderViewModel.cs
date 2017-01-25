using System.ComponentModel;
using System.Data.OleDb;

namespace OMCResultsApp.ViewModels
{
    public class RiderViewModel
    {
        public RiderViewModel()
        { }

        public int RacerId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Sponsors { get; set; }

        public static RiderViewModel Create(OleDbDataReader reader)
        {
            return new RiderViewModel
            {
                RacerId = (int) reader["racer_id"],
                FirstName = reader["fname"].ToString(),
                LastName = reader["lname"].ToString(),
                Number = reader["racing_nbr"].ToString(),
                City = reader["city"].ToString(),
                State = reader["state"].ToString(),
                Sponsors = reader["sponsors"].ToString()
            };
        }
    }
}