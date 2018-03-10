using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Threading;
using static DataScience.Computations;
using static DataScience.Hashmap;

namespace DataScience
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashmap hashmap = new Hashmap();
            string datafile = @"D:\Projects\Jaar 3\C#\DataScience\DataScience\DataScience\Data\userItem.data";

            hashmap.Parsedata(datafile);
            Generalised(hashmap, 1, 1, 2, 107);

            Console.ReadKey();
        }
    }
}
