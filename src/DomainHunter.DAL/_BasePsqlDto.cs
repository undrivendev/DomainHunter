using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainHunter.DAL
{
    public class BasePsqlDto
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
