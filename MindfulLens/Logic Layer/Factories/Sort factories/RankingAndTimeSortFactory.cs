using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Factories
{
    public class RankingAndTimeSortFactory : SortFactory
    {
        public RankingAndTimeSortFactory()
        {
            this.sorters = new Dictionary<(string, string), Tuple<string, string>>()
            {
                { ("ranking", "asc"), new Tuple<string, string>("Ranking", "ASC") },
                { ("ranking", "desc"), new Tuple<string, string>("Ranking", "DESC") },
                { ("time", "asc"), new Tuple<string, string>("CreationDate", "ASC") },
                { ("time", "desc"), new Tuple<string, string>("CreationDate", "DESC") }
            };
        }
    }
}
