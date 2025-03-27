using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class TrialSourceRepository : ISourceRepository
    {
        private List<Source> sources;

        public TrialSourceRepository()
        {
            User creator = new User("Tom", "tom@gmail.com", "tom12345", "tom");
            this.sources = new List<Source>()
            {
                new Source(1, "Very cool source", DateTime.Now, creator, "Apple", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(2, "Very cool source", DateTime.Now, creator, "Cinammon", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(3, "Very cool source", DateTime.Now, creator, "Bean", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(4, "Very cool source", DateTime.Now, creator, "Orange", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(5, "Very cool source", DateTime.Now, creator, "Banana", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(6, "Very cool source", DateTime.Now, creator, "Mango", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(7, "Very cool source", DateTime.Now, creator, "Lemon", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(8, "Very cool source", DateTime.Now, creator, "Pear", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(9, "Very cool source", DateTime.Now, creator, "Watermelon", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true),
                new Source(10, "Very cool source", DateTime.Now, creator, "Kiwi", new string[] {"link 1", "link 2", "link 3", "link 4"}, "Cool author", true)
            };
        }
        public void AddSource(Source source)
        {
            throw new NotImplementedException();
        }

        public void DeleteSource(Source source)
        {
            throw new NotImplementedException();
        }

        public Source[] GetAllSources()
        {
            throw new NotImplementedException();
        }

        public Source[] GetAllSources(int number)
        {
            throw new NotImplementedException();
        }

        public Source[] GetCustomSources()
        {
            throw new NotImplementedException();
        }

        public Source[] GetCustomSources(int number)
        {
            throw new NotImplementedException();
        }

        public Source[] GetOfficialSortedSources(int number, string sortMethod, string sortOrder)
        {
            Source[] list_to_return = sources.ToArray();

            if (sortOrder == "ASC")
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderBy(c => c.CreationDate).ToArray();
                else
                    list_to_return = list_to_return.OrderBy(c => c.Title).ToArray();
            }
            else
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderByDescending(c => c.CreationDate).ToArray();
                else
                    list_to_return = list_to_return.OrderByDescending(c => c.Title).ToArray();
            }
            return list_to_return.Take(number).ToArray();
        }

        public Source[] GetOfficialSources(int number)
        {
            return sources.Take(number).ToArray();
        }

        public Source? GetSourceByAuthorAndTitle(string author, string title)
        {
            throw new NotImplementedException();
        }

        public Source? GetSourceById(int id)
        {
            return sources.FirstOrDefault(s => s.ID == id);
        }

        public Source[] GetSources(string? searchQuery, string? sortMethod, string? sortOrder, bool? isOfficial, int number)
        {
            throw new NotImplementedException();
        }

        public Source[] GetSourcesByUser(User creator)
        {
            throw new NotImplementedException();
        }

        public void MakeSourceOfficial(Source source)
        {
            throw new NotImplementedException();
        }

        public void MakeSourceUnofficial(Source source)
        {
            throw new NotImplementedException();
        }

        public Source[] SearchForSortedSources(string query, bool isOfficial, int number, string sortMethod, string sortOrder)
        {
            List<Source> list_to_return = new List<Source>();
            foreach (Source source in sources)
            {
                if (source.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    list_to_return.Add(source);
            }
            if (sortOrder == "ASC")
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderBy(c => c.CreationDate).ToList();
                else
                    list_to_return = list_to_return.OrderBy(c => c.Title).ToList();
            }
            else
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderByDescending(c => c.CreationDate).ToList();
                else
                    list_to_return = list_to_return.OrderByDescending(c => c.Title).ToList();
            }
            return list_to_return.Take(number).ToArray();
        }

        public Source[] SearchForSources(string query, bool isOfficial, int number)
        {
            List<Source> return_list = new List<Source>();
            foreach (Source source in sources)
            {
                if (source.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    return_list.Add(source);
            }
            return return_list.Take(number).ToArray();
        }

        public void UpdateSource(Source source)
        {
            throw new NotImplementedException();
        }
    }
}
