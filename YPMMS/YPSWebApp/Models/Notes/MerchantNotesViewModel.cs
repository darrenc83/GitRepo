using System.Collections.Generic;
using YPSWebApp.Models.Notes;

namespace YPSWebApp.Models.MerchantNotes
{
    public class MerchantNotesViewModel
    {
        public int MerchantId { get; set; }
        public int NoteId { get; set; }
      
        public List<NotesViewModel> MerchantNotesList { get; set; }
        
    }
}
