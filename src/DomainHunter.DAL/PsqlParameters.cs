﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.DAL
{
    public class PsqlParameters
    {
        public string ConnectionString { get; protected set; }

        public PsqlParameters(string connectionString)
        {
            ConnectionString = String.IsNullOrWhiteSpace(connectionString) ? throw new ArgumentNullException(nameof(connectionString)) : connectionString;
        }
    }
}
