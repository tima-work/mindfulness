using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class TrialCognitiveRepository : ICognitivePartRepository
    {
        private User user;
        private List<CognitivePart> cognitiveParts;

        public TrialCognitiveRepository()
        {
            user = new User("Bob", "bob@gmail.com", "bob12345", "bob");
            cognitiveParts = new List<CognitivePart>()
            {
                new CognitivePart(1, "Hello, how are you? I'm fine, thank you, and what about you? I'm also doing great, thanks. By the way, how is your project going? Oh, thank you for asking, I'm struggling to  finish it", DateTime.Now, user, "Apple", "Bias", "Just overcome it", true),
                new CognitivePart(2, "The bias has number 2", DateTime.Now, user, "Cinammon", "Bias", "Just overcome it", true),
                new CognitivePart(3, "The bias has number 3", DateTime.Now, user, "Bean", "Bias", "Just overcome it", true),
                new CognitivePart(4, "The bias has number 4", DateTime.Now, user, "Orange", "Bias", "Just overcome it", true),
                new CognitivePart(5, "The bias has number 5", DateTime.Now, user, "Banana", "Bias", "Just overcome it", true),
                new CognitivePart(6, "The bias has number 6", DateTime.Now, user, "Mango", "Bias", "Just overcome it", true),
                new CognitivePart(7, "The bias has number 7", DateTime.Now, user, "Lemon", "Bias", "Just overcome it", true),
                new CognitivePart(8, "The bias has number 8", DateTime.Now, user, "Pear", "Bias", "Just overcome it", true),
                new CognitivePart(9, "The bias has number 9", DateTime.Now, user, "Watermelon", "Bias", "Just overcome it", true),
                new CognitivePart(10, "The bias has number 10", DateTime.Now, user, "Kiwi", "Bias", "Just overcome it", true),
                new CognitivePart(11, "The theory has number 11", DateTime.Now, user, "Apple", "Theory", "Just develop it", true),
                new CognitivePart(12, "The theory has number 12", DateTime.Now, user, "Cinammon", "Theory", "Just develop it", true),
                new CognitivePart(13, "The theory has number 13", DateTime.Now, user, "Bean", "Theory", "Just develop it", true),
                new CognitivePart(14, "The theory has number 14", DateTime.Now, user, "Orange", "Theory", "Just develop it", true),
                new CognitivePart(15, "The theory has number 15", DateTime.Now, user, "Banana", "Theory", "Just develop it", true),
                new CognitivePart(16, "The theory has number 16", DateTime.Now, user, "Mango", "Theory", "Just develop it", true),
                new CognitivePart(17, "The theory has number 17", DateTime.Now, user, "Lemon", "Theory", "Just develop it", true),
                new CognitivePart(18, "The theory has number 18", DateTime.Now, user, "Pear", "Theory", "Just develop it", true),
                new CognitivePart(19, "The theory has number 19", DateTime.Now, user, "Watermelon", "Theory", "Just develop it", true),
                new CognitivePart(20, "The bias has number 20", DateTime.Now, user, "Kiwi", "Theory", "Just develop it", true),
            };
    }
        public void AddCognitivePart(CognitivePart cognitivePart)
        {
            throw new NotImplementedException();
        }

        public void DeleteCognitivePart(CognitivePart cognitivePart)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetCognitivePartsByClassname(string classname)
        {
            throw new NotImplementedException();
        }

        public CognitivePart? GetCognitivePartById(int id)
        {
            return cognitiveParts.FirstOrDefault(c => c.ID == id);
        }

        public CognitivePart? GetCognitivePartByTitleAndClassname(string title, string classname)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetCognitivePartsByCreator(User creator, string classname)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetCustomCognitiveParts(string classname)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetOfficialCognitiveParts(string classname, int number)
        {
            return cognitiveParts.Where(c => c.ClassName == classname).Take(number).ToArray();
        }

        public CognitivePart[] GetOfficialSortedCognitiveParts(string classname, int number, string sort, string order)
        {

            CognitivePart[] list_to_return = cognitiveParts.Where(c => c.ClassName == classname).ToArray();

            if (order == "ASC")
            {
                if (sort == "CreationDate")
                    list_to_return = list_to_return.OrderBy(c => c.CreationDate).ToArray();
                else
                    list_to_return = list_to_return.OrderBy(c => c.Title).ToArray();
            }
            else
            {
                if (sort == "CreationDate")
                    list_to_return = list_to_return.OrderByDescending(c => c.CreationDate).ToArray();
                else
                    list_to_return = list_to_return.OrderByDescending(c => c.Title).ToArray();
            }
            return list_to_return.Take(number).ToArray();

        }

        public void MakeCognitivePartOfficial(CognitivePart cognitivePart)
        {
            throw new NotImplementedException();
        }

        public void MakeCognitivePartUnofficial(CognitivePart cognitivePart)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] SearchForCognitiveParts(string query, string classname, bool isOfficial, int number)
        {
            List<CognitivePart> list_to_return = new List<CognitivePart>();
            foreach (CognitivePart cognitivePart in cognitiveParts.Where(c => c.ClassName == classname))
            {
                if (cognitivePart.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    list_to_return.Add(cognitivePart);
            }
            return list_to_return.Take(number).ToArray();
        }

        public CognitivePart[] SearchForSortedCognitiveParts(string query, string classname, bool isOfficial, int number, string sort, string order)
        {
            List<CognitivePart> list_to_return = new List<CognitivePart>();
            foreach (CognitivePart cognitivePart in cognitiveParts.Where(c => c.ClassName == classname))
            {
                if (cognitivePart.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    list_to_return.Add(cognitivePart);
            }
            if (order == "ASC")
            {
                if (sort == "CreationDate")
                    list_to_return =  list_to_return.OrderBy(c => c.CreationDate).ToList();
                else
                    list_to_return =  list_to_return.OrderBy(c => c.Title).ToList();
            }
            else
            {
                if (sort == "CreationDate")
                    list_to_return =  list_to_return.OrderByDescending(c => c.CreationDate).ToList();
                else
                    list_to_return =  list_to_return.OrderByDescending(c => c.Title).ToList();
            }
            return list_to_return.Take(number).ToArray();
        }

        public void UpdateCognitivePart(CognitivePart cognitivePart)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetCognitivePartsByClassname(string classname, int number)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetCustomCognitiveParts(string classname, int number)
        {
            throw new NotImplementedException();
        }

        public CognitivePart[] GetCognitiveParts(string? searchQuery, string? sortMethod, string? sortOrder, bool? isOfficial, string className, int number)
        {
            throw new NotImplementedException();
        }
    }
}
