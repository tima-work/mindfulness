using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IExerciseRepository
    {
        //Exercise[] GetAllExercises(int number);
        //Exercise[] GetOfficialExercises(int number);
        //Exercise[] GetOfficialSortedExercises(int number, string sortMethod, string sortOrder);
        //Exercise[] GetCustomExercises(int number);
        Exercise[] GetExercisesByCreator(User creator);
        //Exercise[] SearchForExercises(string query, bool isOfficial, int number);
        //Exercise[] SearchForSortedExercises(string query, bool isOfficial, int number, string sortMethod, string sortOrder);
        Exercise[] GetExercises(string? query, string? sortMethod, string? sortOrder, bool? isOfficial, int number);
        Exercise? GetExerciseByTitle(string title);
        Exercise? GetExerciseById(int id);
        void AddExercise(Exercise exercise);
        void DeleteExercise(Exercise exercise);
        void UpdateExercise(Exercise exercise);
        void MakeExerciseOfficial(Exercise exercise);
        void MakeExerciseUnofficial(Exercise exercise);
    }
}
