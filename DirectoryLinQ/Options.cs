using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace DirectoryLinQ
{
    internal class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]

        public bool Verbose { get; set; }


        public string? Directory { get; set; }
    }
}
