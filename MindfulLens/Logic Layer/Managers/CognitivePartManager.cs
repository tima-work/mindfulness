using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class CognitivePartManager
    {
        private ICognitivePartRepository cognitivePartRepository;

        public CognitivePartManager(ICognitivePartRepository cognitivePartRepository)
        {
            this.cognitivePartRepository = cognitivePartRepository;
        }

        public void AddCognitivePart(CognitivePart cognitivePart)
        {
            CheckCognitivePartInfo(cognitivePart, false);
            cognitivePartRepository.AddCognitivePart(cognitivePart);
        }

        public void DeleteCognitivePart(CognitivePart cognitivePart)
        {
            cognitivePartRepository.DeleteCognitivePart(cognitivePart);
        }

        //public CognitivePart[] GetAllCognitiveParts()
        //{
        //    return cognitivePartRepository.GetAllCognitiveParts();
        //}

        //public CognitivePart[] GetCognitivePartsByClassname(string classname, int number)
        //{
        //    return cognitivePartRepository.GetCognitivePartsByClassname(classname, number);
        //}

        public CognitivePart[] GetCognitivePartsByCreator(User creator, string classname)
        {
            return cognitivePartRepository.GetCognitivePartsByCreator(creator, classname);
        }

        //public CognitivePart[] SearchForCognitiveParts(string query, string classname, bool isOfficial, int number)
        //{
        //    return cognitivePartRepository.SearchForCognitiveParts(query, classname, isOfficial, number);
        //}

        //public CognitivePart[] SearchForSortedCognitiveParts(string query, string classname, bool isOfficial, int number, string sortMethod, string sortOrder)
        //{
        //    return cognitivePartRepository.SearchForSortedCognitiveParts(query, classname, isOfficial, number, sortMethod, sortOrder);
        //}

        //public CognitivePart[] GetOficcialCognitiveParts(string classname, int number)
        //{
        //    return cognitivePartRepository.GetOfficialCognitiveParts(classname, number);
        //}

        //public CognitivePart[] GetOfficialSortedCognitiveParts(string classname, int number, string sortMethod, string sortOrder)
        //{
        //    return cognitivePartRepository.GetOfficialSortedCognitiveParts(classname, number, sortMethod, sortOrder);
        //}

        //public CognitivePart[] GetCustomCognitiveParts(string classname, int number)
        //{
        //    return cognitivePartRepository.GetCustomCognitiveParts(classname, number);
        //}

        public void UpdateCognitivePart(CognitivePart cognitivePart)
        {
            CheckCognitivePartInfo(cognitivePart, true);
            cognitivePartRepository.UpdateCognitivePart(cognitivePart);
        }

        private void CheckCognitivePartInfo(CognitivePart cognitivePart, bool checkId)
        {
            if (String.IsNullOrWhiteSpace(cognitivePart.Title))
                throw new Exception($"You haven't entered title of {cognitivePart.ClassName.ToLower()}");
            if (String.IsNullOrWhiteSpace(cognitivePart.Text))
                throw new Exception($"You haven't entered text of {cognitivePart.ClassName.ToLower()}");
            CognitivePart? foundCognitivePart = cognitivePartRepository.GetCognitivePartByTitleAndClassname(cognitivePart.Title, cognitivePart.ClassName);
            if (foundCognitivePart != null && (!checkId || cognitivePart.ID != foundCognitivePart.ID))
                throw new Exception($"There is already {cognitivePart.ClassName.ToLower()} with such a title");
        }

        //public CognitivePart[] SortOfficialCogntiveParts(IComparer<Publication> comparer, string classname)
        //{
        //    return SortSomeCognitiveParts(cognitivePartRepository.GetOfficialCognitiveParts(classname), comparer);
        //}

        //public CognitivePart[] SortCustomCogntiveParts(IComparer<Publication> comparer, string classname)
        //{
        //    return SortSomeCognitiveParts(cognitivePartRepository.GetCustomCognitiveParts(classname), comparer);
        //}

        //public CognitivePart[] SortAllCogntiveParts(IComparer<Publication> comparer, string classname)
        //{
        //    return SortSomeCognitiveParts(cognitivePartRepository.GetCognitivePartsByClassname(classname), comparer);
        //}

        //public CognitivePart[] SortSomeCognitiveParts(CognitivePart[] cognitiveParts, IComparer<Publication> comparer)
        //{
        //    Array.Sort(cognitiveParts, comparer);
        //    return cognitiveParts;
        //}

        public CognitivePart? GetCognitivePartById(int id)
        {
            return cognitivePartRepository.GetCognitivePartById(id);
        }

        public CognitivePart[] GetCognitiveParts(string? search_query, string? sortMethod, string? sortOrder, bool? isOfficial, string className, int number)
        {
            return cognitivePartRepository.GetCognitiveParts(search_query, sortMethod, sortOrder, isOfficial, className, number);
        } 
    }
}
