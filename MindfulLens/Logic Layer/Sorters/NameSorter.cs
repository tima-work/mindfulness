using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Sorters
{
    public class NameSorter : IComparer<Publication>
    {
        private int order;
        public NameSorter(int order)
        {
            this.order = order;
        }

        public int Compare(Publication? publication1, Publication? publication2)
        {
            return String.Compare((publication1 as TitledPublication).Title, (publication2 as TitledPublication).Title) * order;
        }
    }
}
