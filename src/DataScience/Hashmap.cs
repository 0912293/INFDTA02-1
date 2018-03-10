using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace DataScience
{
    class Hashmap
    {
        private Dictionary<int, UserPreferences> inner_dict = new Dictionary<int, UserPreferences>();

        public void Add(int key, UserPreferences item)
        {
            if (inner_dict.ContainsKey(key))
            {
                throw new Exception(String.Format("Existing key found at: {0}", key));
            }
            inner_dict.Add(key, item);
        }

        public void Remove(int key)
        {
            if (inner_dict.ContainsKey(key))
            {
                inner_dict.Remove(key);
            }
            else
            {
                throw new KeyNotFoundException(String.Format("No value found at key {0}", key));
            }
        }

        public int GetCount()
        {
            return inner_dict.Count;
        }

        public UserPreferences GetEntry(int key)
        {
            if (inner_dict.ContainsKey(key))
            {
                return inner_dict[key];
            }
            else
            {
                throw new KeyNotFoundException(String.Format("No value found at key {0}", key));
            }
        }

        public Dictionary<int, UserPreferences> GetInner()
        {
            return inner_dict;
        }

        public void Parsedata(string datapath)
        {
            // Add culture data to force Decimal.Parse() to use dot separators.
            CultureInfo culture = new CultureInfo("en-US", true);
            culture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = culture;

            foreach (string entry in File.ReadLines(datapath))
            {
                string[] subEntry = entry.Split(',');
                int user_id = Int16.Parse(subEntry[0]);
                int article = Int16.Parse(subEntry[1]);
                double rating = Double.Parse(subEntry[2]);

                if (inner_dict.ContainsKey(user_id))
                {
                    GetEntry(user_id).Add(article, rating);
                }
                else
                {
                    this.Add(user_id, new UserPreferences());
                    this.GetEntry(user_id).Add(article, rating);
                }
            }
        }

        public static Hashmap ParseLinq(string datapath)
        {
            Hashmap generatedDict = new Hashmap();

            // Step 1: Data
            var lines = File.ReadLines(datapath);

            // Step 2: Query
            var linqDic = lines
                .Select(line => line.Split(','))
                .Select(entry => new { UserId = Int16.Parse(entry[0]), Article = Int16.Parse(entry[1]), Rating = Decimal.Parse(entry[2]) })
                ;

            // Step 3: Execution
            foreach (string line in lines)
            {

            }

            return generatedDict;
        }
    }

    class UserPreferences
    {
        private Dictionary<int, double> inner_dict = new Dictionary<int, double>();

        public void Add(int article, double rating)
        {
            if (inner_dict.ContainsKey(article))
            {
                inner_dict.Remove(article);
            }
            inner_dict.Add(article, rating);
        }

        public Dictionary<int, double> GetInnerDict()
        {
            return inner_dict;
        }

        public double GetValueAtKey(int key)
        {
            if (inner_dict.ContainsKey(key))
            {
                return inner_dict[key];
            }
            else
            {
                throw new KeyNotFoundException(String.Format("No value found at key {0}", key));
            }

        }

        public void GetAllValues()
        {
            foreach (KeyValuePair<int, double> entry in inner_dict)
            {
                Console.WriteLine(String.Format("Key: {0}, value: {1}", entry.Key, entry.Value));
            }
        }
    }
}
