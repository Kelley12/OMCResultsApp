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
        public string Name { get; set; }
        public string Location { get; set; }
        public int Moto1Finish { get; set; }
        public int Moto2Finish { get; set; }
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
                Location = reader["Location"].ToString(),
                //Moto1Finish = String.IsNullOrEmpty(reader["moto_1_finish"].ToString()) ? 99 : (int) reader["moto_1_finish"],
                //Moto2Finish = String.IsNullOrEmpty(reader["moto_2_finish"].ToString()) ? 99 : (int) reader["moto_2_finish"],
                //OverallFinish = String.IsNullOrEmpty(reader["overall_finish"].ToString()) ? 99 : (int) reader["overall_finish"]
            };
        }
    }
}