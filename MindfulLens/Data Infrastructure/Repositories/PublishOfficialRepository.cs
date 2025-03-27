using Logic_Layer.Classes;
using Logic_Layer.Repository_Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class PublishOfficialRepository : ConnectionRepository, IPublishOfficialRepository
    {
        private CognitivePartRepository cognitivePartRepository;
        private ExerciseRepository exerciseRepository;
        private SourceRepository sourceRepository;


        public PublishOfficialRepository(CognitivePartRepository cognitivePartRepository, ExerciseRepository exerciseRepository, SourceRepository sourceRepository)
        {
            this.cognitivePartRepository = cognitivePartRepository;
            this.exerciseRepository = exerciseRepository;
            this.sourceRepository = sourceRepository;
        }


        public void AddPublishOfficial(PublishOfficial publishOfficial)
        {
            DoSimpleAction("INSERT INTO PublishOfficial (Text, CreationDate, PublicationID, PublicationType) VALUES (@Text, @CreationDate, @PublicationID, @PublicationType);", new { publishOfficial.Text, publishOfficial.CreationDate, PublicationID = publishOfficial.Publication.ID, PublicationType = publishOfficial.Publication.GetType().Name });
        }

        public void DeletePublishOfficial(PublishOfficial publishOfficial)
        {
            DoSimpleAction("DELETE FROM PublishOfficial WHERE ID = @ID", new { publishOfficial.ID });
        }

        public void DeletePublishOfficialByPublication(TitledPublication titledPublication)
        {
            DoSimpleAction("DELETE FROM PublishOfficial WHERE PublicationID = @PublicationID AND PublicationType = @PublicationType", new { PublicationID = titledPublication.ID, PublicationType = titledPublication.GetType().Name });
        }

        public PublishOfficial[] GetPublishOfficialByUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM PublishOfficial";
                object parameters = null;
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                string type;
                int id;
                List<PublishOfficial> list_to_return = new List<PublishOfficial>();
                TitledPublication publication;
                Dictionary<int, Publication> cognitiveParts = GetCognitivePartDictionary(sqlQuery, parameters);
                Dictionary<int, Publication> exercises = GetExerciseDictionary(sqlQuery, parameters);
                Dictionary<int, Publication> sources = GetSourceDictionary(sqlQuery, parameters);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        type = reader["PublicationType"].ToString();
                        id = Convert.ToInt32(reader["PublicationID"]);
                        switch (type)
                        {
                            case nameof(CognitivePart):
                                publication = (CognitivePart)cognitiveParts[id];
                                break;
                            case nameof(Exercise):
                                publication = (Exercise)exercises[id];
                                break;
                            case nameof(Source):
                                publication = (Source)sources[id];
                                break;
                            default:
                                publication = null;
                                break;
                        }

                        if (publication != null || publication.Creator.Id != user.Id)
                            continue;

                        list_to_return.Add(new PublishOfficial(
                            Convert.ToInt32(reader["ID"]),
                            reader["Text"].ToString(),
                            Convert.ToDateTime(reader["CreationDate"]),
                            publication
                            ));
                    }

                    return list_to_return.ToArray(); 
                }
            }
        }

        public PublishOfficial[] GetAllPublishOfficial()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM PublishOfficial";
                object parameters = null;
                SqlCommand cmd = new SqlCommand("SELECT * FROM PublishOfficial", connection);
                string type;
                int id;
                List<PublishOfficial> list_to_return = new List<PublishOfficial>();
                TitledPublication publication;
                Dictionary<int, Publication> cognitiveParts = GetCognitivePartDictionary(sqlQuery, parameters);
                Dictionary<int, Publication> exercises = GetExerciseDictionary(sqlQuery, parameters);
                Dictionary<int, Publication> sources = GetSourceDictionary(sqlQuery, parameters);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        type = reader["PublicationType"].ToString();
                        id = Convert.ToInt32(reader["PublicationID"]);
                        switch (type)
                        {
                            case nameof(CognitivePart):
                                publication = (CognitivePart)cognitiveParts[id];
                                break;
                            case nameof(Exercise):
                                publication = (Exercise)exercises[id];
                                break;
                            case nameof(Source):
                                publication = (Source)sources[id];
                                break;
                            default:
                                publication = null;
                                break;
                        }

                        list_to_return.Add(new PublishOfficial(
                            Convert.ToInt32(reader["ID"]),
                            reader["Text"].ToString(),
                            Convert.ToDateTime(reader["CreationDate"]),
                            publication
                            ));
                    }

                    return list_to_return.ToArray();
                }
            }
        }

        private Dictionary<int, Publication> GetCognitivePartDictionary(string sqlQuery, object parameters = null)
        {
            return cognitivePartRepository.GetCognitivePartDictionary($@"SELECT CognitiveParts.ID, CognitiveParts.Text, CognitiveParts.CreationDate, CognitiveParts.CreatorID, CognitiveParts.Title, CognitiveParts.WayOfHandling, CognitiveParts.ClassName, CognitiveParts.IsOfficial
                                                                                         INNER JOIN (
                                                                                         {sqlQuery}
                                                                                         ) AS Result ON Result.PublicationID = CognitiveParts.ID;", parameters);
        }

        private Dictionary<int, Publication> GetSourceDictionary(string sqlQuery, object parameters = null)
        {
            return sourceRepository.GetSourceDictionary($@"SELECT Sources.ID, Sources.Text, Sources.CreationDate, Sources.CreatorID, Sources.Title, Sourses.Author, Sources.IsOfficial
                                                                                         INNER JOIN (
                                                                                         {sqlQuery}
                                                                                         ) AS Result ON Result.PublicationID = Sources.ID;", parameters);
        }

        private Dictionary<int, Publication> GetExerciseDictionary(string sqlQuery, object parameters = null)
        {
            return exerciseRepository.GetExerciseDictionary($@"SELECT Exercises.ID, Exercises.Text, Exercises.CreationDate, Exercises.CreatorID, Exercises.Title, Exercises.IsOfficial
                                                                                         INNER JOIN (
                                                                                         {sqlQuery}
                                                                                         ) AS Result ON Result.PublicationID = Exercises.ID;", parameters);
        }
    }
}
