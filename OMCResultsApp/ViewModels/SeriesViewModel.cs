using System.ComponentModel;
using System.Data.OleDb;

namespace OMCResultsApp.ViewModels
{
    public class SeriesViewModel
    {
        public SeriesViewModel()
        { }

        public int SeriesId { get; set; }
        [DisplayName("Series Name")]
        public string SeriestName { get; set; }

        public static SeriesViewModel Create(OleDbDataReader reader)
        {
            return new SeriesViewModel
            {
                SeriesId = (int) reader["series_id"],
                SeriestName = reader["series_desc"].ToString()
            };
        }
    }
}