using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;

namespace OMCResultsApp.ViewModels
{
    public class ResultsViewModel
    {
        public ResultsViewModel()
        { }

        public int EventId { get; set; }
        [DisplayName("Event Name")]
        public string EventName { get; set; }
        [DisplayName("Event Date")]
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }
        public int ClassId { get; set; }
        [DisplayName("Class Description")]
        public string ClassDescription { get; set; }
        public int RacerId { get; set; }
        [DisplayName("Rider")]
        public string Name { get; set; }
        [DisplayName("Rider Number")]
        public string Number { get; set; }
        public string Location { get; set; }
        [DisplayName("Moto 1")]
        public int Moto1Finish { get; set; }
        [DisplayName("Moto 2")]
        public int Moto2Finish { get; set; }
        [DisplayName("Overall")]
        public int OverallFinish { get; set; }

        public static ResultsViewModel Create(OleDbDataReader reader)
        {
            return new ResultsViewModel
            {
                EventId = (int) reader["event_id"],
                EventName = reader["event_name"].ToString(),
                EventDate = (DateTime) reader["event_date"],
                ClassId = (int) reader["class_id"],
                ClassDescription = reader["class_desc"].ToString(),
                RacerId = (int) reader["racer_id"],
                Name = reader["Name"].ToString(),
                Number = reader["racing_nbr"].ToString(),
                Location = reader["Location"].ToString(),
                Moto1Finish = String.IsNullOrEmpty(reader["moto_1_finish"].ToString()) ? 89 : Int32.Parse(reader["moto_1_finish"].ToString()),
                Moto2Finish = String.IsNullOrEmpty(reader["moto_2_finish"].ToString()) ? 89 : Int32.Parse(reader["moto_2_finish"].ToString()),
                OverallFinish = String.IsNullOrEmpty(reader["overall_finish"].ToString()) ? 89 : Int32.Parse(reader["overall_finish"].ToString())
            };
        }
    }
}