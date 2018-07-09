using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DscBuildTools.Model;
using DscBuildTools.Types;

namespace ConsoleTester
{
    internal static class Program
    {
        public static void Main(string[] args)
        {

            StringBuilder sb = new StringBuilder();
            MOFWriter writer = new MOFWriter(new StringWriter(sb));

            Console.WriteLine(sb);

            PressAnyKey();
        }
        private static void PressAnyKey()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

    }
}
