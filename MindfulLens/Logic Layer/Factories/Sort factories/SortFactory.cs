using Logic_Layer.Classes;
using Logic_Layer.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Factories
{
    public abstract class SortFactory
    {
        protected Dictionary<(string, string), Tuple<string, string>> sorters;


        public Tuple<string, string>? GetSorter(string initialMethod, string intialOrder)
        {
            string a = initialMethod.ToLower();
            string b = intialOrder.ToLower();

            foreach (KeyValuePair<(string, string), Tuple<string, string>> pair in sorters)
            {
                if (pair.Key.Item1 == a && pair.Key.Item2 == b)
                    return pair.Value;
            }

            return null;
        }
    }
}
