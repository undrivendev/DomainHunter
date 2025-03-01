﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    public class Domain : BaseModel
    {
        public string Name { get; set; }
        public string Tld { get; set; }
        public DomainStatus Status { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? Checked { get; set; }
        
        public override string ToString()
            => $"{Name}.{Tld}";
    }
}
