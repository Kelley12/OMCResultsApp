using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;

namespace OMCResultsApp.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel()
        { }

        public int EventId { get; set; }
        [DisplayName("Event Name")]
        public string EventName { get; set; }
        [DisplayName("Event Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        public static EventViewModel Create(OleDbDataReader reader)
        {
            return new EventViewModel
            {
                EventId = (int) reader["event_id"],
                EventName = reader["event_name"].ToString(),
                EventDate = (DateTime) reader["event_date"]
            };
        }
    }
}