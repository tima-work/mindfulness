using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IContentRepository
    {
        //Content[] GetAllContent(int number);
        //Content[] SearchForAllContent(string query, int number);
        //Content[] GetContentByClassname(string classname, int number);
        Content[] GetContent(string? search_query, string? sortMethod, string? sortOrder, string? classname, int number);
        //Content[] GetContentByCreator(User creator);
        //Content[] SearchForContentWithClassname(string query, string classname, int number);
        //Content[] SearchForSortedContent(string query, int number, string sortMethod, string sortOrder);
        Content? GetContentByTitleAndClassname(string title, string classname);
        Content? GetContentById(int id);
        void AddContent(Content content);
        void DeleteContent(Content content);
        void UpdateContent(Content content);
    }
}
