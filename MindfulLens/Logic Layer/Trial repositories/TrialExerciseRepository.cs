using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logic_Layer
{
    public class TrialExerciseRepository : IExerciseRepository
    {
        private List<Exercise> exercises;
        public TrialExerciseRepository()
        {
            User creator = new User("Tom", "tom@gmail.com", "tom12345", "tom");
            this.exercises = new List<Exercise>()
            {
                new Exercise(1, "Very cool source", DateTime.Now, creator, "Apple",  true),
                new Exercise(2, "Very cool source", DateTime.Now, creator, "Cinammon", true),
                new Exercise(3, "Very cool source", DateTime.Now, creator, "Bean", true),
                new Exercise(4, "Very cool source", DateTime.Now, creator, "Orange", true),
                new Exercise(5, "Very cool source", DateTime.Now, creator, "Banana", true),
                new Exercise(6, "Very cool source", DateTime.Now, creator, "Mango", true),
                new Exercise(7, "Very cool source", DateTime.Now, creator, "Lemon", true),
                new Exercise(8, "Very cool source", DateTime.Now, creator, "Pear", true),
                new Exercise(9, "Very cool source", DateTime.Now, creator, "Watermelon", true),
                new Exercise(10, "Very cool source", DateTime.Now, creator, "Kiwi", true)
            };
        }
        public void AddExercise(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public void DeleteExercise(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetAllExercises()
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetAllExercises(int number)
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetCustomExercises()
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetCustomExercises(int number)
        {
            throw new NotImplementedException();
        }

        public Exercise? GetExerciseById(int id)
        {
            return exercises.FirstOrDefault(e => e.ID == id);
        }

        public Exercise? GetExerciseByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetExercises(string? query, string? sortMethod, string? sortOrder, int number, bool isOfficial)
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetExercises(string? query, string? sortMethod, string? sortOrder, bool? isOfficial, int number)
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetExercisesByCreator(User creator)
        {
            throw new NotImplementedException();
        }

        public Exercise[] GetOfficialExercises(int number)
        {
            return exercises.Take(number).ToArray();
        }

        public Exercise[] GetOfficialSortedExercises(int number, string sortMethod, string sortOrder)
        {
            Exercise[] list_to_return = exercises.ToArray();

            if (sortOrder == "ASC")
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderBy(c => c.CreationDate).ToArray();
                else
                    list_to_return = list_to_return.OrderBy(c => c.Title).ToArray();
            }
            else
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderByDescending(c => c.CreationDate).ToArray();
                else
                    list_to_return = list_to_return.OrderByDescending(c => c.Title).ToArray();
            }
            return list_to_return.Take(number).ToArray();
        }

        public void MakeExerciseOfficial(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public void MakeExerciseUnofficial(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public Exercise[] SearchForExercises(string query, bool isOfficial, int number)
        {
            List<Exercise> list_to_return = new List<Exercise>();
            foreach (Exercise exercise in exercises)
            {
                if (exercise.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    list_to_return.Add(exercise);
            }
            return list_to_return.Take(number).ToArray();
        }

        public Exercise[] SearchForSortedExercises(string query, bool isOfficial, int number, string sortMethod, string sortOrder)
        {
            List<Exercise> list_to_return = new List<Exercise>();
            foreach (Exercise exercise in exercises)
            {
                if (exercise.Title.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                    list_to_return.Add(exercise);
            }
            if (sortOrder == "ASC")
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderBy(c => c.CreationDate).ToList();
                else
                    list_to_return = list_to_return.OrderBy(c => c.Title).ToList();
            }
            else
            {
                if (sortMethod == "CreationDate")
                    list_to_return = list_to_return.OrderByDescending(c => c.CreationDate).ToList();
                else
                    list_to_return = list_to_return.OrderByDescending(c => c.Title).ToList();
            }
            return list_to_return.Take(number).ToArray();
        }

        public void UpdateExercise(Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}
