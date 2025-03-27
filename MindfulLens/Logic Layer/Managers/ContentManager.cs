using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class ContentManager
    {
        private IContentRepository contentRepository;

        public ContentManager(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public void AddContent(Content content)
        {
            CheckContentInfo(content, false);
            contentRepository.AddContent(content);
        }

        public void DeleteContent(Content content)
        {
            contentRepository.DeleteContent(content);
        }

        //public Content[] GetAllContent()
        //{
        //    return contentRepository.GetAllContent();
        //}

        //public Content[] GetContentByClassname(string classname)
        //{
        //    return contentRepository.GetContentByClassname(classname);
        //}

        //public Content[] GetContentByCreator(User creator)
        //{
        //    return contentRepository.GetContentByCreator(creator);
        //}

        //public Content[] GetAllContent(int number)
        //{
        //    return contentRepository.GetAllContent(number);
        //}

        //public Content[] GetAllSortedContent(int number, string sortMethod, string sortOrder)
        //{

        //}

        //public Content[] SearchForAllContent(string query, int number)
        //{
        //    return contentRepository.SearchForAllContent(query, number);
        //}

        //public Content[] SearchForAllSortedContent(string query, int number, string sortMethod, string sortOrder)
        //{

        //}

        //public Content[] SearchForFilteredContent(string query, int number, string classname)
        //{

        //}

        //public Content[] SearchForFilteredSortedContent(string query, int number, string classname, string sortMethod, string sortOrder)
        //{

        //}

        //public Content[] GetFilteredContent(string classname, int number)
        //{

        //} 

        //public Content[] GetFilteredSortedContent(string classname, string sortMethod, string sortOrder, int number)
        //{

        //}
        public Content[] GetContent(string? search_query, string? sortMethod, string? sortOrder, string? className, int number)
        {
            return contentRepository.GetContent(search_query, sortMethod, sortOrder, className, number);
        }





        //public Content[] SearchForContent(Content[] content, string query)
        //{
        //    return content.Where(c => c.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase) || c.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToArray();
        //}

        public void UpdateContent(Content content)
        {
            CheckContentInfo(content, true);
            contentRepository.UpdateContent(content);
        }

        private void CheckContentInfo(Content content, bool checkId)
        {
            if (String.IsNullOrWhiteSpace(content.Title))
                throw new Exception($"You haven't entered title of {content.ClassName.ToLower()}");
            if (String.IsNullOrWhiteSpace(content.Text))
                throw new Exception($"You haven't entered text of {content.ClassName.ToLower()}");
            if (content.ClassName != "News" && content.ClassName != "Interview")
                throw new Exception("Classname should be either \"News\" or \"Interview\"");
            Content? found_content = contentRepository.GetContentByTitleAndClassname(content.Title, content.ClassName);
            if (found_content != null && (!checkId || found_content.ID != content.ID))
                throw new Exception($"There is already {content.ClassName.ToLower()} with such a title");
        }

        //public Content[] SortContent(IComparer<Publication> comparer, string classname)
        //{
        //    Content[] content = contentRepository.GetContentByClassname(classname);
        //    Array.Sort(content, comparer);
        //    return content;
        //}

        //public Content[] SortSomeContent(Content[] content, IComparer<Publication> comparer)
        //{
        //    Array.Sort(content, comparer);
        //    return content;
        //}

        public Content? GetContentById(int id)
        {
            return contentRepository.GetContentById(id);
        }

        //public Content[] FilterContentByClassname(Content[] content, string classname, int number)
        //{
        //    return content.Where(c => c.ClassName == classname).Take(number).ToArray();
        //}


    }
}
