using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Sorters
{
    public class RankingSorter : IComparer<Publication>
    {
        private int order;
        public RankingSorter(int order)
        {
            this.order = order;
        }

        public int Compare(Publication? review1, Publication? review2)
        {
            int a = (review1 as Review).Ranking - (review2 as Review).Ranking;
            if (a == 0)
                return 0;
            return a / Math.Abs(a) * order;
        }
    }
}
