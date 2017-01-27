using System.ComponentModel;
using System.Data.OleDb;

namespace OMCResultsApp.ViewModels
{
    public class ClassViewModel
    {
        public ClassViewModel()
        { }

        public int EventId { get; set; }
        public int ClassId { get; set; }
        [DisplayName("Class Description")]
        public string ClassDescription { get; set; }

        public static ClassViewModel Create(OleDbDataReader reader)
        {
            return new ClassViewModel
            {
                EventId = (int) reader["event_id"],
                ClassId = (int) reader["class_id"],
                ClassDescription = reader["class_desc"].ToString()
            };
        }
    }
}