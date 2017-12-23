using System;


namespace YPSWebApp.Models.Notes
{
    public class NotesViewModel
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
