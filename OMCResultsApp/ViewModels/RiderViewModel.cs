using System.ComponentModel;
using System.Data.OleDb;

namespace OMCResultsApp.ViewModels
{
    public class RiderViewModel
    {
        public RiderViewModel()
        { }

        public int RacerId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Sponsors { get; set; }

        public static RiderViewModel Create(OleDbDataReader reader)
        {
            return new RiderViewModel
            {
                RacerId = (int) reader["racer_id"],
                Name = reader["Name"].ToString(),
                Number = reader["racing_nbr"].ToString(),
                City = reader["city"].ToString(),
                State = reader["state"].ToString(),
                Sponsors = reader["sponsors"].ToString()
            };
        }
    }
}