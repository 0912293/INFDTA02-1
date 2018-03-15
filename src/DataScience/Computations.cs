using System;
using System.Collections;
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
    internal class Computations
    {
        public static double Similarity(double devisor)
        {
            return 1 / (1 + devisor);
        }

        public static double Generalised(Hashmap ratings, int r, int user_one, int user_two, int item)
        {
            var user_one_ratings = ratings.GetEntry(user_one).GetInnerDict();
            var user_two_ratings = ratings.GetEntry(user_two).GetInnerDict();

            double sums = 0;

            if (user_two_ratings.ContainsKey(item) && user_one_ratings.ContainsKey(item))
                sums += Pow(Abs(user_one_ratings[item] - user_two_ratings[item]), r);
            else
                throw new Exception("No item for users");


            var answer = Pow(sums, 1 / r);
            return Similarity(answer);
        }

        public static double Pearson(Hashmap ratings, int user_one, int user_two)
        {
            double sx = 0, sy = 0, summ = 0, ssx = 0, ssy = 0;
            var n = 0;

            var user_one_ratings = ratings.GetEntry(user_one).GetInnerDict();
            var user_two_ratings = ratings.GetEntry(user_two).GetInnerDict();

            var similairRatingOne = new List<double>();
            var similairRatingTwo = new List<double>();

            foreach (var kvpair in user_one_ratings)
                if (user_two_ratings.ContainsKey(kvpair.Key))
                {
                    similairRatingOne.Add(user_one_ratings[kvpair.Key]);
                    similairRatingTwo.Add(user_two_ratings[kvpair.Key]);
                }

            if (similairRatingOne.Count == similairRatingTwo.Count) n = similairRatingOne.Count;
            if (n == 0) throw new Exception("No items for users");

            for (var i = 0; i < n; i++)
            {
                summ += similairRatingOne[i] * similairRatingTwo[i];
                sx += similairRatingOne[i];
                sy += similairRatingTwo[i];
                ssx += Pow(similairRatingOne[i], 2);
                ssy += Pow(similairRatingTwo[i], 2);
            }                 

            var c1 = summ - sx * sy / n;
            var c2 = Sqrt(ssx - Pow(sx, 2) / n);
            var c3 = Sqrt(ssy - Pow(sy, 2) / n);
            return c1 / (c2 * c3);
        }

        public static double Pearson(Hashmap ratings, int user_one, int user_two, Boolean nn)
        {
            double sx = 0, sy = 0, summ = 0, ssx = 0, ssy = 0;
            var n = 0;

            var user_one_ratings = ratings.GetEntry(user_one).GetInnerDict();
            var user_two_ratings = ratings.GetEntry(user_two).GetInnerDict();

            var similairRatingOne = new List<double>();
            var similairRatingTwo = new List<double>();

            foreach (var kvpair in user_one_ratings)
                if (user_two_ratings.ContainsKey(kvpair.Key))
                {
                    similairRatingOne.Add(user_one_ratings[kvpair.Key]);
                    similairRatingTwo.Add(user_two_ratings[kvpair.Key]);
                }

            if (similairRatingOne.Count == similairRatingTwo.Count) n = similairRatingOne.Count;
            if (n == 0&&nn==false) throw new Exception("No items for users");

            for (var i = 0; i < n; i++)
            {
                summ += similairRatingOne[i] * similairRatingTwo[i];
                sx += similairRatingOne[i];
                sy += similairRatingTwo[i];
                ssx += Pow(similairRatingOne[i], 2);
                ssy += Pow(similairRatingTwo[i], 2);
            }

            var c1 = summ - sx * sy / n;
            var c2 = Sqrt(ssx - Pow(sx, 2) / n);
            var c3 = Sqrt(ssy - Pow(sy, 2) / n);
            return c1 / (c2 * c3);
        }

        public static double Cosine(Hashmap ratings, int user_one, int user_two)
        {                     
            double summ = 0, ssx = 0, ssy = 0;
            var n = 0;

            var user_one_ratings = ratings.GetEntry(user_one).GetInnerDict();
            var user_two_ratings = ratings.GetEntry(user_two).GetInnerDict();

            var similairRatingOne = new List<double>();
            var similairRatingTwo = new List<double>();

            foreach (var kvpair in user_one_ratings)
                if (user_two_ratings.ContainsKey(kvpair.Key)&&user_one_ratings.ContainsKey(kvpair.Key))
                {
                    similairRatingOne.Add(user_one_ratings[kvpair.Key]);
                    similairRatingTwo.Add(user_two_ratings[kvpair.Key]);
                }
                else
                {
                    throw new Exception("No sim");
                }

            if (similairRatingOne.Count == similairRatingTwo.Count) n = similairRatingOne.Count;

            for (var i = 0; i < n; i++)
            {
                summ += similairRatingOne[i] * similairRatingTwo[i];
                ssx += Pow(similairRatingOne[i], 2);
                ssy += Pow(similairRatingTwo[i], 2);
            }                 
            return summ / (Sqrt(ssx) * Sqrt(ssy));
        }

        public static Dictionary<int, double> NearestNeighbour(Hashmap ratings, int target, double treshhold, int K)
        {
            var inner = ratings.GetInner();                      
            var nearest = new Dictionary<int, double>(); 
            
            foreach (var kvpair in inner)
                if (kvpair.Key != target)
                {
                    var pCo = Pearson(ratings, kvpair.Key, target,true);
                    if (pCo >= treshhold) nearest.Add(kvpair.Key,pCo);
                }      
            var sortedNeighbours = nearest.OrderByDescending(x => x.Value).Take(K).ToDictionary(pair => pair.Key, pair => pair.Value);
            return sortedNeighbours;
        }

        public static double Predicted(Dictionary<int, double> neighbours, Hashmap ratings, int item)
        {
            double c1 = 0;
            double c2 = 0;

            foreach (var kvpair in neighbours)
                if (ratings.GetEntry(kvpair.Key).GetInnerDict().ContainsKey(item))
                {
                    c1 += neighbours[kvpair.Key] * ratings.GetEntry(kvpair.Key).GetValueAtKey(item);
                    c2 += neighbours[kvpair.Key];
                }

            return c1 / c2;
        }

        public static Dictionary<int, double> TopNRec(Hashmap ratings, int user, int N, double treshhold, int K, int same)
        {
            var userMovieId = new List<int>();
            var nnMovieId = new List<int>();
            var nn = NearestNeighbour(ratings, user, treshhold, K);
            var rec = new Dictionary<int, double>();

            foreach (var kvpair in ratings.GetInner())
                if (kvpair.Key == user)
                    foreach (var innerpair in kvpair.Value.GetInnerDict())
                        userMovieId.Add(innerpair.Key); 

            foreach (var kvpair in ratings.GetInner())
                foreach (var innerpair in kvpair.Value.GetInnerDict())
                    if (!userMovieId.Contains(innerpair.Key))nnMovieId.Add(innerpair.Key);

            var moviesIds = nnMovieId.GroupBy(x => x).Where(group => group.Count() > same-1).Select(group => group.Key);

            foreach (var movie in moviesIds)
            {
                rec.Add(movie, Predicted(nn, ratings, movie));
            }

            var topN = rec.OrderByDescending(x => x.Value).Take(N).ToDictionary(pair => pair.Key, pair => pair.Value);

            return topN;
        }    
    }
}
