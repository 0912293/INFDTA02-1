using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;

namespace DataScience
{
    class Computations
    {
        public static double Similarity(double devisor)
        {
            return (1 / (1 + devisor));
        }

        public static double Generalised(int r, List<Tuple<int,int>> ratings)
        //Similarity(Pow(Pow(Abs(p - q), r), (1/r));
        {
            double sums = 0;
            foreach(var item in ratings)
            {
                sums += Pow(Abs(item.Item1 - item.Item2), r);
            }

            double answer = Pow(sums, (1 / r));
            return Similarity(answer);
        }

        public static void Pearson(Hashmap user_ratings, int art_number_one, int art_number_two)
        {
            List<UserPreferences> shared_ratings = new List<UserPreferences>();

            foreach (var item in user_ratings.GetInner())
            {
                if (user_ratings.GetInner().Where(x => x.Key == item.Key).Count() != 0)
                {
                    shared_ratings.Add(item);
                }
            }
        }
    }
}
