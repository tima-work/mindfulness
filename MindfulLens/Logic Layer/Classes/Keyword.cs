using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class Keyword
    {
        public string keyWord { get; private set; }
        public decimal Ranking { get; private set; }

        public Keyword(string keyWord, decimal ranking)
        {
            this.keyWord = keyWord;
            this.Ranking = ranking;
        }
    }
}
