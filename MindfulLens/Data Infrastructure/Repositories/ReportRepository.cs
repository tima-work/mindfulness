using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class ReportRepository : ConnectionRepository, IReportRepository
    {
        private UserRepository userRepository;
        private ReviewRepository reviewRepository;
        private ForumRepository forumRepository;
        private SourceRepository sourceRepository;
        private CognitivePartRepository cognitivePartRepository;
        private ExerciseRepository exerciseRepository;

        public ReportRepository(UserRepository userRepository, ReviewRepository reviewRepository, ForumRepository forumRepository, SourceRepository sourceRepository, CognitivePartRepository cognitivePartRepository, ExerciseRepository exerciseRepository)
        {
            this.userRepository = userRepository;
            this.reviewRepository = reviewRepository;
            this.forumRepository = forumRepository;
            this.sourceRepository = sourceRepository;
            this.cognitivePartRepository = cognitivePartRepository;
            this.exerciseRepository = exerciseRepository;
        }
        public void AddReport(Report report)
        {
            DoSimpleAction("INSERT INTO Reports (CreationDate, Text, CreatorID, ReportedPublicationID, ReportedPublicationType, Reason) VALUES (@CreationDate, @Text, @CreatorID, @ReportedPublicationID, @ReportedPublicationType, @Reason);", new { CreationDate = report.CreationDate, Text = report.Text, CreatorID = report.Creator.Id, ReportedPublicationID = report.ReportedPublication.ID, ReportedPublicationType = report.ReportedPublication.GetType().Name, Reason = report.Reason });
        }

        public void DeleteReport(Report report)
        {
            DoSimpleAction("DELETE FROM Reports WHERE ID = @ID", report);
        }

        public void DeleteReportsByPublication(Publication publication)
        {
            DoSimpleAction("DELETE FROM Reports WHERE ReportedPublicationType = @Type AND ReportedPublicationID = @ID;", new {Type = publication.GetType().Name, ID = publication.ID});
        }

        //public Report[] GetReports(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Reports WHERE CreatorID IS NOT NULL";
        //    object parameters = new { Number = number };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Report[] GetAutoReports(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Reports WHERE CreatorID IS NULL";
        //    object parameters = new { Number = number };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public Report[] GetAllReportsByCreator(User creator)
        {
            string sqlQuery = "SELECT * FROM Reports WHERE CreatorID = @CreatorID";
            object parameters = new { CreatorID = creator.Id };
            return InitConverterAndQueryDatabase(sqlQuery, parameters);
        }

        //public Report[] SearchForReports(string query, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Reports WHERE CreatorID IS NOT NULL AND (Text LIKE %@Query% OR Reason LIKE %@Query%);";
        //    object parameters = new { Number = number, Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        private Report[] CastToReport(Publication[] publications)
        {
            Report[] reports = new Report[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                reports[i] = (Report)publications[i];
            return reports;
        }

        //public Report[] SearchForAutoReports(string query, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Reports WHERE CreatorID IS NULL AND (Text LIKE %@Query% OR Reason LIKE %@Query%);";
        //    object parameters = new { Number = number, Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);

        //}


        private Report[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            return CastToReport(GetData("SELECT * FROM " + sqlQuery, GetConverter(sqlQuery, parameters), parameters));
        }

        private Report[] GetReportsWithNumber(string sqlQuery, object parameters, int number)
        {
            return CastToReport(GetData($"SELECT TOP {number} * FROM " + sqlQuery, GetConverter(sqlQuery, parameters), parameters));
        }

        public Report? GetReportById(int id)
        {
            string sqlQuery = "Reports WHERE ID = @ID;";
            object parameters = new { ID = id };
            Report[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        private ReportConverter GetConverter(string sqlQuery, object parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    StringBuilder reviewStringBuilder = new StringBuilder("Reviews WHERE ");
                    StringBuilder forumStringBuilder = new StringBuilder("ForumPublications WHERE ");
                    StringBuilder cognitiveStringBuilder = new StringBuilder("CognitiveParts WHERE ");
                    StringBuilder sourceStringBuilder = new StringBuilder("Sources WHERE ");
                    StringBuilder exerciseStringBuilder = new StringBuilder("Exercises WHERE ");

                    SqlCommand cmd = new SqlCommand("SELECT ReportedPublicationID, ReportedPublicationType FROM " + sqlQuery, connection);

                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                        }
                    }

                    int id;
                    string type;

                    Dictionary<string, StringBuilder> helpfulDict = new Dictionary<string, StringBuilder>()
                {
                    { "Review", reviewStringBuilder },
                    { "ForumPublication", forumStringBuilder },
                    { "CognitivePart", cognitiveStringBuilder },
                    { "Source", sourceStringBuilder },
                    { "Exercise", exerciseStringBuilder }
                };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["ReportedPublicationID"]);
                            type = reader["ReportedPublicationType"].ToString();

                            if (helpfulDict.ContainsKey(type))
                            {
                                helpfulDict[type].Append($"ID = {id} OR ");
                            }
                        }
                    }

                    foreach (StringBuilder stringBuilder in helpfulDict.Values)
                    {
                        stringBuilder.Append("0 = 1");
                    }

                    Dictionary<int, Publication> dict1 = reviewRepository.GetReviewDictionary(reviewStringBuilder.ToString(), parameters);
                    Dictionary<int, Publication> dict2 = forumRepository.GetForumPublicationsDictionary(forumStringBuilder.ToString(), parameters);
                    Dictionary<int, Publication> dict3 = sourceRepository.GetSourceDictionary(sourceStringBuilder.ToString(), parameters);
                    Dictionary<int, Publication> dict4 = cognitivePartRepository.GetCognitivePartDictionary(cognitiveStringBuilder.ToString(), parameters);
                    Dictionary<int, Publication> dict5 = exerciseRepository.GetExerciseDictionary(exerciseStringBuilder.ToString(), parameters);

                    Dictionary<int, User> users = userRepository.GetUserDictionaryForNullableCreator(sqlQuery, parameters);
                    return new ReportConverter(users, dict1, dict2, dict3, dict4, dict5);
                    //return new ReportConverter(userRepository.GetUserDictionaryForNullableCreator(sqlQuery, parameters), reviewRepository.GetReviewDictionary(reviewStringBuilder.ToString(), parameters), forumRepository.GetForumPublicationsDictionary(forumStringBuilder.ToString(), parameters), sourceRepository.GetSourceDictionary(sourceStringBuilder.ToString(), parameters), cognitivePartRepository.GetCognitivePartDictionary(cognitiveStringBuilder.ToString(), parameters), exerciseRepository.GetExerciseDictionary(exerciseStringBuilder.ToString(), parameters));

                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }

        public Report[] GetReports(string? search_query, string? sortMethod, string? sortOrder, bool isAuto, int number)
        {
            string sqlQuery;
            object parameters;
            string ifAuto;

            if (isAuto)
                ifAuto = "";
            else
                ifAuto = "NOT ";

            if (search_query != null)
            {
                sqlQuery = $"Reports WHERE CreatorID IS {ifAuto}NULL AND (Reason LIKE '%' + @Query + '%' OR Text LIKE '%' + @Query + '%')";
                parameters = new { Query = search_query };
            }
            else
            {
                sqlQuery = $"Reports WHERE CreatorID IS {ifAuto}NULL";
                parameters = new {  };
            }

            if (sortMethod != null && sortOrder != null)
            {
                sqlQuery = $"{sqlQuery} ORDER BY {sortMethod} {sortOrder}";
            }

            return GetReportsWithNumber(sqlQuery, parameters, number);
        }
    }
}
