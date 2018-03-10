using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;
using static DataScience.Hashmap;

namespace DataScience
{
    class Computations
    {
        public static double Similarity(double devisor)
        {
            return (1 / (1 + devisor));
        }

        public static double Generalised(Hashmap ratings, int r, int user_one, int user_two, int item)
        {
            Dictionary<int, double> user_one_ratings = ratings.GetEntry(user_one).GetInnerDict();
            Dictionary<int, double> user_two_ratings = ratings.GetEntry(user_two).GetInnerDict();

            double sums = 0;
            foreach (KeyValuePair<int, double> kvpair in user_one_ratings)
            {
                if (user_two_ratings.ContainsKey(kvpair.Key))
                {
                    sums += Pow(Abs(user_one_ratings[kvpair.Key] - user_two_ratings[kvpair.Key]), r);
                }
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
                    //shared_ratings.Add(item);
                }
            }
        }

        //public static double Generalised(Hashmap ratings, int r, int user_one, int user_two, int item)
        //{
        //    Dictionary<int, double> user_one_ratings = ratings.GetEntry(user_one).GetInnerDict();
        //    Dictionary<int, double> user_two_ratings = ratings.GetEntry(user_two).GetInnerDict();

        //    double sums = 0;
        //    foreach (KeyValuePair<int, double> kvpair in user_one_ratings)
        //    {
        //        if (user_two_ratings.ContainsKey(kvpair.Key))
        //        {
        //            sums += Pow(Abs(user_one_ratings[kvpair.Key] - user_two_ratings[kvpair.Key]), r);
        //        }
        //    }

        //    double answer = Pow(sums, (1 / r));
        //    return Similarity(answer);
        //}
    }
}
