using Logic_Layer.Classes;
using Logic_Layer.Publication_cleaners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Layer.Keywords_finders
{
    public class GraphTextRankKeywordsFinder : IKeywordsFinder
    {
        private IPublicationCleaner[] publicationCleaners;
        public GraphTextRankKeywordsFinder(IPublicationCleaner[] publicationCleaners)
        {
            this.publicationCleaners = publicationCleaners;
        }
        public Keyword[] FindKeywords(string text)
        {
            string cleaner_text = text;
            foreach (IPublicationCleaner cleaner in publicationCleaners)
                cleaner_text = cleaner.MakeTextCleaner(cleaner_text);
            int keywords_number = CountKeywordsNumber(cleaner_text);
            var words = cleaner_text.Split(' ').Select(w => w.Trim().ToLower()).ToList();
            var wordGraph = new Dictionary<string, HashSet<string>>();

            for (int i = 0; i < words.Count - 1; i++)
            {
                if (!wordGraph.ContainsKey(words[i]))
                    wordGraph[words[i]] = new HashSet<string>();

                wordGraph[words[i]].Add(words[i + 1]);

                if (!wordGraph.ContainsKey(words[i + 1]))
                    wordGraph[words[i + 1]] = new HashSet<string>();

                wordGraph[words[i + 1]].Add(words[i]);
            }

            var scores = RankWords(wordGraph);

            return scores
                .Where(pair => !string.IsNullOrWhiteSpace(pair.Key) && pair.Value != null)
                .OrderByDescending(pair => pair.Value)
                .Take(keywords_number)
                .Select(pair => new Keyword(pair.Key, pair.Value)).ToArray();
            //return scores.Where(pair => !string.IsNullOrWhiteSpace(pair.Key) && pair.Value != null).OrderByDescending(pair => pair.Value).Take(keywords_number).ToArray();

        }

        private int CountKeywordsNumber(string clean_text)
        {
            string word_count_pattern = @"\b\w+\b";
            Regex regex = new Regex(word_count_pattern, RegexOptions.IgnoreCase);
            int keywords_number = Convert.ToInt32(regex.Matches(clean_text).Count * 0.2);
            if (keywords_number < 1)
                return 1;
            return keywords_number;
        }


        private Dictionary<string, decimal> RankWords(Dictionary<string, HashSet<string>> graph)
        {
            var scores = graph.ToDictionary(pair => pair.Key, pair => 1.0m);
            const decimal dampingFactor = 0.85m;
            const int iterations = 100;

            for (int i = 0; i < iterations; i++)
            {
                var newScores = new Dictionary<string, decimal>();

                foreach (var word in graph.Keys)
                {
                    decimal score = (1 - dampingFactor);

                    foreach (var neighbor in graph[word])
                    {
                        score += dampingFactor * (scores[neighbor] / graph[neighbor].Count);
                    }

                    newScores[word] = score;
                }

                scores = newScores;
            }

            return scores;
        }
    }
}
