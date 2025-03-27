using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class SourceManager
    {
        private ISourceRepository sourceRepository;

        public SourceManager(ISourceRepository sourceRepository)
        {
            this.sourceRepository = sourceRepository;
        }

        public void AddSource(Source source)
        {
            CheckSourceInfo(source, false);
            sourceRepository.AddSource(source);
        }

        public void DeleteSource(Source source)
        {
            sourceRepository.DeleteSource(source);
        }

        //public Source[] GetAllSources(int number)
        //{
        //    return sourceRepository.GetAllSources(number);
        //}

        public Source[] GetSourcesByUser(User creator)
        {
            return sourceRepository.GetSourcesByUser(creator);
        }

        public void UpdateSource(Source source)
        {
            CheckSourceInfo(source, true);
            sourceRepository.UpdateSource(source);
        }


        //public Source[] SearchForSources(string query, bool isOfficial, int number)
        //{
        //    return sourceRepository.SearchForSources(query, isOfficial, number);
        //}

        //public Source[] SearchForSortedSources(string query, bool isOfficial, int number, string sortMethod, string sortOrder)
        //{
        //    return sourceRepository.SearchForSortedSources(query, isOfficial, number, sortMethod, sortOrder);
        //}

        //public Source[] SearchForSources(Source[] sources, string query)
        //{
        //    return sources.Where(s => s.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase) || s.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase) || s.Author.Contains(query, StringComparison.InvariantCultureIgnoreCase) || s.GetLinks().FirstOrDefault(l => l.Contains(query, StringComparison.InvariantCultureIgnoreCase)) != null).ToArray();
        //}

        //public Source[] GetOfficialSources(int number)
        //{
        //    return sourceRepository.GetOfficialSources(number);
        //}

        //public Source[] GetOfficialSortedSources(int number, string sortMethod, string sortOrder)
        //{
        //    return sourceRepository.GetOfficialSortedSources(number, sortMethod, sortOrder);
        //}



        //public Source[] GetCustomSources(int number)
        //{
        //    return sourceRepository.GetCustomSources(number);
        //}

        private void CheckSourceInfo(Source source, bool checkId)
        {
            if (String.IsNullOrWhiteSpace(source.Author))
                throw new Exception("You haven't entered author of  source");
            if (String.IsNullOrWhiteSpace(source.Text))
                throw new Exception("You haven't entered description of source");
            Source? found_source = sourceRepository.GetSourceByAuthorAndTitle(source.Author, source.Title);
            if (found_source != null && (!checkId || found_source.ID != source.ID))
                throw new Exception("There is already a source from this author with such a name");
        }

        //public Source[] SortOfficialSources(IComparer<Publication> comparer)
        //{
        //    return SortSomeSources(sourceRepository.GetOfficialSources(), comparer);
        //}

        //public Source[] SortCustomSources(IComparer<Publication> comparer)
        //{
        //    return SortSomeSources(sourceRepository.GetCustomSources(), comparer);
        //}

        //public Source[] SortAllSources(IComparer<Publication> comparer)
        //{
        //    return SortSomeSources(sourceRepository.GetAllSources(), comparer);
        //}

        //public Source[] SortSomeSources(Source[] sources, IComparer<Publication> comparer)
        //{
        //    Array.Sort(sources, comparer);
        //    return sources;
        //}

        public Source? GetSourceById(int id)
        {
            return sourceRepository.GetSourceById(id);
        }

        public Source[] GetSources(string? search_query, string? sortMethod, string? sortOrder, bool? isOfficial, int number)
        {
            return sourceRepository.GetSources(search_query, sortMethod, sortOrder, isOfficial, number);
        }
    }
}
