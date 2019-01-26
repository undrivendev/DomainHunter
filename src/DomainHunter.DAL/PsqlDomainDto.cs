using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainHunter.DAL
{
    [Table("domain")]
    public class PsqlDomainDto : BasePsqlDto
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("status")]
        public byte Status { get; set; }
        [Column("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
