using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class ExerciseManager
    {
        private IExerciseRepository exerciseRepository;


        public ExerciseManager(IExerciseRepository exerciseRepository)
        {
            this.exerciseRepository = exerciseRepository;
        }


        public void AddExercise(Exercise exercise)
        {
            CheckExerciseInfo(exercise, false);
            exerciseRepository.AddExercise(exercise);
        }

        public void RemoveExercise(Exercise exercise)
        {
            exerciseRepository.DeleteExercise(exercise);
        }

        public void UpdateExercise(Exercise exercise)
        {
            CheckExerciseInfo(exercise, true);
            exerciseRepository.UpdateExercise(exercise);
        }

        //public Exercise[] GetAllExercises(int number)
        //{
        //    return exerciseRepository.GetAllExercises(number);
        //}

        //public Exercise[] GetOfficialExercises(int number)
        //{
        //    return exerciseRepository.GetOfficialExercises(number);
        //}

        //public Exercise[] GetOfficialSortedExercises(int number, string sortMethod, string sortOrder)
        //{
        //    return exerciseRepository.GetOfficialSortedExercises(number, sortMethod, sortOrder);
        //}

        //public Exercise[] GetCustomExercises(int number)
        //{
        //    return exerciseRepository.GetCustomExercises(number);
        //}

        public Exercise[] GetExercisesByCreator(User creator)
        {
            return exerciseRepository.GetExercisesByCreator(creator);
        }

        private void CheckExerciseInfo(Exercise exercise, bool checkId)
        {
            if (String.IsNullOrWhiteSpace(exercise.Text))
                throw new Exception("You haven't entered text of exercise");
            if (String.IsNullOrEmpty(exercise.Title))
                throw new Exception("You haven't entered title of exercise");
            Exercise? found_exercise = exerciseRepository.GetExerciseByTitle(exercise.Title);
            if (found_exercise != null && (!checkId || found_exercise.ID != exercise.ID))
                throw new Exception("There is already exercise with such a name");
        }

        //public Exercise[] SearchForExercises(string query, bool isOfficial, int number)
        //{
        //    return exerciseRepository.SearchForExercises(query, isOfficial, number);
        //}

        //public Exercise[] SearchForSortedExercises(string query, bool isOfficial, int number, string sortMethod, string sortOrder)
        //{
        //    return exerciseRepository.SearchForSortedExercises(query, isOfficial, number, sortMethod, sortOrder);
        //}

        //public Exercise[] SearchForExercise(Exercise[] exercises, string query)
        //{
        //    return exercises.Where(e => e.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase) || e.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToArray();
        //}

        //public Exercise[] SortOfficialExercises(IComparer<Exercise> comparer)
        //{
        //    return SortSomeExercises(exerciseRepository.GetOfficialExercises(), comparer);
        //}

        //public Exercise[] SortCustomExercises(IComparer<Exercise> comparer)
        //{
        //    return SortSomeExercises(exerciseRepository.GetCustomExercises(), comparer);
        //}

        //public Exercise[] SortAllExercises(IComparer<Exercise> comparer)
        //{
        //    return SortSomeExercises(exerciseRepository.GetAllExercises(), comparer);
        //}

        //public Exercise[] SortSomeExercises(Exercise[] exercises, IComparer<Exercise> comparer)
        //{
        //    Array.Sort(exercises, comparer);
        //    return exercises;
        //}

        public Exercise? GetExerciseById(int id)
        {
            return exerciseRepository.GetExerciseById(id);
        }

        public Exercise[] GetExercises(string? search_query, string? sortMethod, string? sortOrder, bool? isOfficial, int number)
        {
            return exerciseRepository.GetExercises(search_query, sortMethod, sortOrder, isOfficial, number);
        }
    }
}
