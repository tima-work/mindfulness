using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Infrastructure.Repositories
{
    public class ExerciseRepository : ConnectionRepository, IExerciseRepository
    {
        private UserRepository userRepository;

        public ExerciseRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddExercise(Exercise exercise)
        {
            DoSimpleAction("INSERT INTO Exercises (Text, CreationDate, CreatorID, Title, IsOfficial) VALUES (@Text, @CreationDate, @CreatorID, @Title, @IsOfficial);", new {Text = exercise.Text, CreationDate = exercise.CreationDate, CreatorID = exercise.Creator.Id, Title = exercise.Title, IsOfficial = Convert.ToInt32(exercise.IsOfficial)});
        }

        public void DeleteExercise(Exercise exercise)
        {
            DoSimpleAction("DELETE FROM Exercises WHERE ID = @ID;", exercise);
        }

        //public Exercise[] GetAllExercises(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Exercises;";
        //    object parameters = new { Number = number};
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public Exercise? GetExerciseByTitle(string title)
        {
            string sqlQuery = "Exercises WHERE Title = @Title;";
            object parameters = new { Title = title };
            Exercise[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        //public Exercise[] GetOfficialExercises(int number)
        //{
        //    string sqlQuery = "Exercises WHERE IsOfficial = 1";
        //    object parameters = new { Number = number };
        //    return GetExercisesWithNumber(sqlQuery, parameters, number);
        //}

        //public Exercise[] GetOfficialSortedExercises(int number, string sortMethod, string sortOrder)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Exercises WHERE IsOfficial = 1 ORDER BY @SortMethod @SortOrder;";
        //    object parameters = new { Number = number, SortMethod = sortMethod, SortOrder = sortOrder };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public Exercise[] GetExercisesByCreator(User creator)
        {
            string sqlQuery = "SELECT * FROM Exercises WHERE CreatorID = @CreatorID;";
            object parameters = new { CreatorId = creator.Id };
            return InitConverterAndQueryDatabase(sqlQuery, parameters);
        }

        //public Exercise[] GetCustomExercises(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Exercises WHERE IsOfficial = 0;";
        //    object parameters = new { Number = number };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Exercise[] SearchForExercises(string query, bool isOfficial, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Exercises WHERE IsOfficial = @IsOfficial AND (Text LIKE %@Query% OR Title LIKE %@Query%);";
        //    object parameters = new { Number = number, IsOfficial = Convert.ToInt32(isOfficial), Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Exercise[] SearchForSortedExercises(string query, bool isOfficial, int number, string sortMethod, string sortOrder)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Exercises WHERE IsOfficial = @IsOfficial AND (Text LIKE %@Query% OR Title LIKE %@Query%) ORDER BY @SortMethod @SortOrder;";
        //    object parameters = new { Number = number, IsOfficial = Convert.ToInt32(isOfficial), Query = query, SortMethod = sortMethod, SortOrder = sortOrder };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public void UpdateExercise(Exercise exercise)
        {
            DoSimpleAction("UPDATE Exercises SET Text = @Text, Title = @Title WHERE ID = @ID", exercise);
        }

        public void MakeExerciseOfficial(Exercise exercise)
        {
            ChangeExerciseOfficiality(exercise, true);
        }

        public void MakeExerciseUnofficial(Exercise exercise)
        {
            ChangeExerciseOfficiality(exercise, false);
        }

        private void ChangeExerciseOfficiality(Exercise exercise, bool isOfficial)
        {
            int a = Convert.ToInt32(isOfficial);
            DoSimpleAction("UPDATE Exercises SET IsOfficial = @IsOfficial WHERE ID = @ID;", new { IsOfficial = a, ID = exercise.ID });
        }

        private Exercise[] CastToExercise(Publication[] publications)
        {
            Exercise[] exercises = new Exercise[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                exercises[i] = (Exercise)publications[i];
            return exercises;
        }

        public Exercise? GetExerciseById(int id)
        {
            string sqlQuery = "Exercises WHERE ID = @ID;";
            object parameters = new { ID = id };
            Exercise[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        public Dictionary<int, Publication> GetExerciseDictionary(string sqlQuery, object parameters = null)
        {
            //string sqlQuery = "SELECT * FROM Exercises;";
            //object parameters = null;
            ExerciseConverter converter = new ExerciseConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return GetDictionary(sqlQuery, converter, parameters);
        }

        private Exercise[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            ExerciseConverter converter = new ExerciseConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToExercise(GetData($"SELECT * FROM " + sqlQuery, converter, parameters));
        }

        private Exercise[] GetExercisesWithNumber(string sqlQuery, object parameters, int number)
        {
            ExerciseConverter converter = new ExerciseConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToExercise(GetData($"SELECT TOP {number} * FROM " + sqlQuery, converter, parameters));
        }

        public Exercise[] GetExercises(string? search_query, string? sortMethod, string? sortOrder, bool? isOfficial, int number)
        {
            string sqlQuery;
            object parameters;
            if (search_query != null)
            {
                if (isOfficial != null)
                {
                    sqlQuery = "Exercises WHERE IsOfficial = @IsOfficial AND (Text LIKE '%' + @Query + '%' OR Title LIKE '%' + @Query + '%')";
                    parameters = new { IsOfficial = Convert.ToInt32(isOfficial), Query = search_query };
                }
                else
                {
                    sqlQuery = "Exercises WHERE Text LIKE '%' + @Query + '%' OR Title LIKE '%' + @Query + '%'";
                    parameters = new { Query = search_query };
                }
            }
            else
            {
                if (isOfficial != null)
                {
                    sqlQuery = "Exercises WHERE IsOfficial = @IsOfficial";
                    parameters = new { IsOfficial = isOfficial };
                }
                else
                {
                    sqlQuery = "Exercises";
                    parameters = new { };
                }
            }
            if (sortMethod != null && sortOrder != null)
            {
                sqlQuery = $"{sqlQuery} ORDER BY {sortMethod} {sortOrder}";
            }

            return GetExercisesWithNumber(sqlQuery, parameters, number);
        }
    }
}
