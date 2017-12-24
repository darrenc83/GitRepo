using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Models
{
    [Table("AcquiringBank")]
    public class AcquiringBankModel
    {
        public int Id { get; set; }
        public string Bank { get; set; }
    }
}
