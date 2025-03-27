using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Sorters
{
    public class DateSorter : IComparer<Publication>
    {
        private int order;
        public DateSorter(int order)
        {
            this.order = order;
        }
        public int Compare(Publication? publication1, Publication? publication2)
        {
            return DateTime.Compare(publication1.CreationDate, publication2.CreationDate) * order;
        }
    }
}
