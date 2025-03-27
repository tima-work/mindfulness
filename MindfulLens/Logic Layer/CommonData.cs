using Logic_Layer.Classes;
using Logic_Layer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class CommonData
    {
        public const string publicationDeleted = "[deleted]";
        public const string userBanned = "[banned]";

        public static Tuple<string?, string?> CheckSort(string? sort, string? order, SortFactory sortFactory)
        {
            if (sort != null && order != null)
            {
                Tuple<string, string>? sort_setup = sortFactory.GetSorter(sort, order);
                if (sort_setup != null)
                {
                    return sort_setup;
                }
            }

            return new Tuple<string?, string?>(null, null);
        }

        public static bool CheckForShowMoreButton<T>(ref T[] array, int number)
        {
            int length = array.Length;
            if (length > number)
            {
                Array.Resize(ref array, array.Length - 1);
                return true;
            }
            return false;
        }
    }
}
