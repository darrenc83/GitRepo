using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Core.Models
{
    [Table("Notes")]
    public class NotesModel
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
