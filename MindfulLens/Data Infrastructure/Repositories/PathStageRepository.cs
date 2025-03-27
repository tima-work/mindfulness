using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class PathStageRepository : ConnectionRepository, IPathStageRepository
    {
        private PersonalPathRepository personalPathRepository;
        private SourceRepository sourceRepository;
        private CognitivePartRepository cognitivePartRepository;
        private ExerciseRepository exerciseRepository;

        public PathStageRepository(PersonalPathRepository personalPathRepository, SourceRepository sourceRepository, CognitivePartRepository cognitivePartRepository, ExerciseRepository exerciseRepository)
        {
            this.personalPathRepository = personalPathRepository;
            this.sourceRepository = sourceRepository;
            this.cognitivePartRepository = cognitivePartRepository;
            this.exerciseRepository = exerciseRepository;

        }

        public void AddPathStage(PathStage pathStage, PersonalPath personalPath)
        {
            DoSimpleAction("INSERT INTO PathStages (PathID, Place, ActionID, ActionType) VALUES (@PathID, @Place, @ActionID, @ActionType);", new { PathID = personalPath.Id, Place = pathStage.Place, ActionID = pathStage.Action.ID, ActionType = pathStage.Action.GetType().Name });
        }

        public void DeletePathStage(PathStage pathStage)
        {
            DoSimpleAction("DELETE FROM PathStages WHERE ID = @ID;", pathStage.Id);
        }

        public void DeletePathStagesByAction(Exercise action)
        {
            string action_type = action.GetType().Name;
            DoSimpleAction("DELETE FROM PathStages WHERE ActionID = @ActionID AND ActionType = @ActionType", new { ActionID = action.ID, ActionType = action_type });
        }

        public void DeletePathStagesByPersonalPath(PersonalPath personalPath)
        {
            DoSimpleAction("DELETE FROM PathStages WHERE PathID = @PathID;", new { PathID = personalPath.Id });
        }

        //public void DeletePathStagesByUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        public Exercise[] GetActionsByPersonalPath(PersonalPath personalPath)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM PathStages WHERE PathID = @PathID ORDER BY Place ASC;";
                object parameters = new { PathID = personalPath.Id };
                Dictionary<int, Publication> sources = sourceRepository.GetSourceDictionary($@"SELECT Sources.ID, Sources.Text, Sources.CreationDate, Sources.CreatorID, Sources.Title, Sourses.Author, Sources.IsOfficial
                                                                                         INNER JOIN (
                                                                                         {sqlQuery}
                                                                                         ) AS Result ON Result.ActionID = Sources.ID;", parameters);
                Dictionary<int, Publication> cognitiveParts = cognitivePartRepository.GetCognitivePartDictionary($@"SELECT CognitiveParts.ID, CognitiveParts.Text, CognitiveParts.CreationDate, CognitiveParts.CreatorID, CognitiveParts.Title, CognitiveParts.WayOfHandling, CognitiveParts.ClassName, CognitiveParts.IsOfficial
                                                                                         INNER JOIN (
                                                                                         {sqlQuery}
                                                                                         ) AS Result ON Result.ActionID = CognitiveParts.ID;", parameters);
                Dictionary<int, Publication> exercises = exerciseRepository.GetExerciseDictionary($@"SELECT Exercises.ID, Exercises.Text, Exercises.CreationDate, Exercises.CreatorID, Exercises.Title, Exercises.IsOfficial
                                                                                         INNER JOIN (
                                                                                         {sqlQuery}
                                                                                         ) AS Result ON Result.ActionID = Exercises.ID;", parameters);
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                if (parameters != null)
                {
                    foreach (var property in parameters.GetType().GetProperties())
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                }

                List<Exercise> actions = new List<Exercise>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int action_id = Convert.ToInt32(reader["ActionID"]);
                        string action_type = reader["ActionType"].ToString();
                        Exercise action;
                        switch (action_type)
                        {
                            case "Source":
                                action = (Source)sources[action_id];
                                break;
                            case "CognitivePart":
                                action = (CognitivePart)cognitiveParts[action_id];
                                break;
                            case "Exercise":
                                action = (Exercise)exercises[action_id];
                                break;
                            default:
                                action = null;
                                break;
                        }
                        actions.Add(action);
                    }
                }
                return actions.ToArray();
            }
        }

        public PathStage? GetLastPathStage(PersonalPath personalPath)
        {
            throw new NotImplementedException();
        }

        public PathStage? GetNextPathStage(PersonalPath personalPath)
        {
            throw new NotImplementedException();
        }

        //public PathStage[] GetPathStagesByUser(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
