using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WireCodingChallenge.Lib;

namespace WireCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var appPath = Settings.ApplicationPath();
            var inputPath = Path.Combine(appPath, Settings.Get("InputPath"));
            var outPath = Path.Combine(appPath, Settings.Get("OutputPath"));

            ProcessFile.Out(inputPath, outPath); 

        }
    }
}
