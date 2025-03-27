using Logic_Layer.Classes;
using Logic_Layer.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Factories
{
    public class AlphabetAndTimeSortFactory : SortFactory
    {
        public AlphabetAndTimeSortFactory()
        {
            this.sorters = new Dictionary<(string, string), Tuple<string, string>>()
            {
                { ("alphabet", "asc"), new Tuple<string, string>("Title", "ASC") },
                { ("alphabet", "desc"), new Tuple<string, string>("Title", "DESC") },
                { ("time", "asc"), new Tuple<string, string>("CreationDate", "ASC") },
                { ("time", "desc"), new Tuple<string, string>("CreationDate", "DESC") },
            };
        }
    }
}
