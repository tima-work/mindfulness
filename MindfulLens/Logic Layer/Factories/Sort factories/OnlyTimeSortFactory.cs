using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Factories
{
    public class OnlyTimeSortFactory : SortFactory
    {
        public OnlyTimeSortFactory()
        {
            this.sorters = new Dictionary<(string, string), Tuple<string, string>>()
            {
                { ("time", "asc"), new Tuple<string, string>("CreationDate", "ASC") },
                { ("time", "desc"), new Tuple<string, string>("CreationDate", "DESC") },
            };
        }
    }
}
