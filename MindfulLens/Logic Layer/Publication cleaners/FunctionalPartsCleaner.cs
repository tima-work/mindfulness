using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Layer.Publication_cleaners
{
    public class FunctionalPartsCleaner : IPublicationCleaner
    {
        public string MakeTextCleaner(string text)
        {
            string cleaner_text = text;
            string pattern;
            List<string> stopWords = new List<string>() { "on the other hand", " as soon as", " as long as", "not only ", " as a result", "even though", "provided that", " as though", "or not", "but also", "and also", "nor yet", "or else ", "just as", "so also", "than less", "in contrast", "in addition", "in conclusion", "no one", "and", "but", "or", "nor", "for", "yet", "so", "although", "because", "since", "unless", "until", "while", "whereas", "after", "before", "if", " as", "though", "whenever", "wherever", "both", "either", "neither", "not", "only", "also", "whether", "however", "therefore", "moreover", "nevertheless", "consequently", "thus", "hence", "accordingly", "nonetheless", "meanwhile", "similarly", "otherwise", "besides", "furthermore", "then", "henceforth", "conversely", "thereafter", "eventually", "thence", "forthwith", "notwithstanding", "whatever", "whichever", "whoever", "whomever", "rather", "than", "more", "additionally", "subsequently", "likewise", "alternatively", "on", "in", "at", "by", "with", "from", "to", "into", "onto", "upon", "under", "over", "across", "through", "around", "among", "between", "against", "towards", "along", "amid", "behind", "below", "beneath", "beside", "beyond", "during", "inside", "outside", "past", "throughout", "within", "without", "near", "above", "amongst", "despite", "underneath", "the", "a", "an", "this", "that", "these", "those", "my", "your", "his", "her", "its", "our", "their", "some", "any", "each", "every", "all", "much", "many", "several", "few", "little", "most", "less", "fewer", "such", "what", "which", "whose", "another", "i", "you", "he", "she", "it", "we", "they", "me", "him", "us", "them", "myself", "yourself", "himself", "herself", "itself", "ourselves", "yourselves", "themselves", "mine", "yours", "hers", "ours", "theirs", "oneself", "someone", "somebody", "something", "anybody", "anyone", "anything", "everybody", "everyone", "everything", "nobody", "none", "nothing", "be", "have", "do", "will", "would", "shall", "should", "can", "could", "may", "might", "must", "ought", "dare", "need", "used", "being", "been", "am", "is", "are", "was", "were", "has", "having", "does", "did", "had", "one", "of" };
            foreach (string word in stopWords)
            {
                pattern = $@"\b{word.Trim()}\b";
                cleaner_text = Regex.Replace(cleaner_text, pattern, "", RegexOptions.IgnoreCase);
            }
            return cleaner_text;
        }
    }
}
