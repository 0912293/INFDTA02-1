using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Hashmap hashmap = new Hashmap();
            string datafile = @"I:\Datascience\src\DataScience\Data\userItem.data";
            string datafile2 = @"I:\Datascience\src\DataScience\Data\ml-latest-small\ratings.csv";
            hashmap.Parsedata(datafile2,true);
                                        
//            Console.WriteLine(Generalised(hashmap, 2, 1, 2, 31)); //1 = manhat 2 = eucli
//            Console.WriteLine(Pearson(hashmap,1,2));

//            var nn = NearestNeighbour(hashmap, 1, 0, 1263);
//            foreach (var neighbour in nn)
//            {
//                  Console.WriteLine(neighbour.Key+":key Val:"+neighbour.Value);
//            }
//                        Console.WriteLine(Predicted(nn, hashmap, 10))
;
            var recs = TopNRec(hashmap, 1, 10, 0.35, 25, 3);
            foreach (var i in recs)
            {
                Console.WriteLine(i.Key+":key value: "+i.Value);
            }
                                             
            stopWatch.Stop();
            Console.WriteLine("Elapsed in ms: " + stopWatch.ElapsedMilliseconds.ToString());

            Console.ReadKey();
        }
    }
}
