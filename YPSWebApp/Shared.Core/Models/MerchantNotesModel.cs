using System.Collections.Generic;

namespace Shared.Core.Models
{
    public class MerchantNotesModel
    {
        public int MerchantId { get; set; }
        public int NoteId { get; set; }
      
        public List<NotesModel> MerchantNotesList { get; set; }
        
    }
}
