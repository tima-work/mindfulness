using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class TrialContentRepository : IContentRepository
    {
        private List<Content> content;

        public TrialContentRepository()
        {
            User user = new User("Bob", "bob@gmail.com", "bob12345", "bob");
            content = new List<Content>()
            {
                //new Content(1, "Very cool news", DateTime.Now, user, "News 1", "News"),
                //new Content(2, "Very cool news", DateTime.Now, user, "News 2", "News"),
                //new Content(3, "Very cool news", DateTime.Now, user, "News 3", "News"),
                //new Content(4, "Very cool news", DateTime.Now, user, "News 4", "News"),
                //new Content(5, "Very cool news", DateTime.Now, user, "News 5", "News"),
                //new Content(6, "Very cool news", DateTime.Now, user, "News 6", "News"),
                //new Content(7, "Very cool news", DateTime.Now, user, "News 7", "News"),
                //new Content(8, "Very cool news", DateTime.Now, user, "News 8", "News"),
                //new Content(9, "Very cool news", DateTime.Now, user, "News 9", "News"),
                //new Content(10, "Very cool news", DateTime.Now, user, "News 10", "News"),
                //new Content(11, "Very cool interview", DateTime.Now, user, "Interview 1", "Interview"),
                //new Content(12, "Very cool interview", DateTime.Now, user, "Interview 2", "Interview"),
                //new Content(13, "Very cool interview", DateTime.Now, user, "Interview 3", "Interview"),
                //new Content(14, "Very cool interview", DateTime.Now, user, "Interview 4", "Interview"),
                //new Content(15, "Very cool interview", DateTime.Now, user, "Interview 5", "Interview"),
                //new Content(16, "Very cool interview", DateTime.Now, user, "Interview 6", "Interview"),
                //new Content(17, "Very cool interview", DateTime.Now, user, "Interview 7", "Interview"),
                //new Content(18, "Very cool interview", DateTime.Now, user, "Interview 8", "Interview"),
                //new Content(19, "Very cool interview", DateTime.Now, user, "Interview 9", "Interview"),
                //new Content(20, "Very cool interview", DateTime.Now, user, "Interview 10", "Interview"),
            };
        }

        public void AddContent(Content content)
        {
            throw new NotImplementedException();
        }

        public void DeleteContent(Content content)
        {
            throw new NotImplementedException();
        }

        public Content[] GetAllContent(int number)
        {
            return content.Take(number).ToArray();
        }

        public Content[] GetContent(string? search_query, string? classname, string? sortMethod, string? sortOrder, int number)
        {
            throw new NotImplementedException();
        }

        public Content[] GetContentByClassname(string classname, int number)
        {
            return content.Where(c => c.ClassName == classname).Take(number).ToArray();
        }

        public Content? GetContentById(int id)
        {
            return content.FirstOrDefault(c => c.ID == id);
        }

        public Content? GetContentByTitleAndClassname(string title, string classname)
        {
            throw new NotImplementedException();
        }

        public Content[] SearchForAllContent(string query, int number)
        {
            List<Content> list_to_return = new List<Content>();
            foreach (Content content_piece in content)
            {
                if (content_piece.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    list_to_return.Add(content_piece);
            }
            return list_to_return.Take(number).ToArray();
        }

        public Content[] SearchForContentWithClassname(string query, string classname)
        {
            throw new NotImplementedException();
        }

        public Content[] SearchForContentWithClassname(string query, string classname, int number)
        {
            throw new NotImplementedException();
        }

        public void UpdateContent(Content content)
        {
            throw new NotImplementedException();
        }
    }
}
