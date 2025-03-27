using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface ICognitivePartRepository
    {
        //CognitivePart[] GetAllCognitiveParts();
        //CognitivePart[] GetCognitivePartsByClassname(string classname, int number);
        CognitivePart[] GetCognitivePartsByCreator(User creator, string classname);
        //CognitivePart[] SearchForCognitiveParts(string query, string classname, bool isOfficial, int number);
        //CognitivePart[] SearchForSortedCognitiveParts(string query, string classname, bool isOfficial, int number, string sortMethod, string sortOrder);
        //CognitivePart[] GetOfficialCognitiveParts(string classname, int number);
        //CognitivePart[] GetOfficialSortedCognitiveParts(string classname, int number, string sortMethod, string sortOrder);
        //CognitivePart[] GetCustomCognitiveParts(string classname, int number);
        CognitivePart[] GetCognitiveParts(string? searchQuery, string? sortMethod, string? sortOrder, bool? isOfficial, string className, int number);
        CognitivePart? GetCognitivePartByTitleAndClassname(string title, string classname);
        CognitivePart? GetCognitivePartById(int id);
        void AddCognitivePart(CognitivePart cognitivePart);
        void DeleteCognitivePart(CognitivePart cognitivePart);
        void UpdateCognitivePart(CognitivePart cognitivePart);
        void MakeCognitivePartOfficial(CognitivePart cognitivePart);
        void MakeCognitivePartUnofficial(CognitivePart cognitivePart);
    }
}
