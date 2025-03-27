using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface ISourceRepository
    {
        //Source[] GetAllSources(int number);
        Source[] GetSourcesByUser(User creator);
        //Source[] SearchForSources(string query, bool isOfficial, int number);
        //Source[] SearchForSortedSources(string query, bool isOfficial, int number, string sortMethod, string sortOrder);
        //Source[] GetOfficialSources(int number);
        //Source[] GetOfficialSortedSources(int number, string sortMethod, string sortOrder);
        //Source[] GetCustomSources(int number);
        Source[] GetSources(string? searchQuery, string? sortMethod, string? sortOrder, bool? isOfficial, int number);
        Source? GetSourceByAuthorAndTitle(string author, string title);
        Source? GetSourceById(int id);
        void AddSource(Source source);
        void DeleteSource(Source source);
        void UpdateSource(Source source);
        void MakeSourceOfficial(Source source);
        void MakeSourceUnofficial(Source source);
    }
}
