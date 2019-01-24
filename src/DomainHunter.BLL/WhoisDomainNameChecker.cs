using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DomainHunter.BLL
{
    public class WhoisDomainNameChecker : IDomainNameChecker
    {
        public bool CheckName(string name)
        {
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "whois.exe";
            process.Start();
            process.WaitForExit();// Waits here for the process to exit.

            return false;
        }
    }
}
