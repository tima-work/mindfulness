using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Converters
{
    public class ReportConverter : IDTOConverter
    {
        private Dictionary<int, User> users;
        private Dictionary<int, Publication> reviews;
        private Dictionary<int, Publication> forumPublications;
        private Dictionary<int, Publication> sources;
        private Dictionary<int, Publication> cognitiveParts;
        private Dictionary<int, Publication> exercises;

        public ReportConverter(Dictionary<int, User> users, Dictionary<int, Publication> reviews, Dictionary<int, Publication> forumPublications, Dictionary<int, Publication> sources, Dictionary<int, Publication> cognitiveParts, Dictionary<int, Publication> exercises)
        {
            this.users = users;
            this.reviews = reviews;
            this.forumPublications = forumPublications;
            this.sources = sources;
            this.cognitiveParts = cognitiveParts;
            this.exercises = exercises;
        }

        public Publication ConvertFromDTO(SqlDataReader reader)
        {
            int publication_id = Convert.ToInt32(reader["ReportedPublicationID"]);
            string publication_type = reader["ReportedPublicationType"].ToString();
            Publication? reportedPublication;
            switch(publication_type)
            {
                case "Review":
                    reportedPublication = (Review)reviews[publication_id];
                    break;
                case "ForumPublication":
                    reportedPublication = (ForumPublication)forumPublications[publication_id];
                    break;
                case "Source":
                    reportedPublication = (Source)sources[publication_id];
                    break;
                case "CognitivePart":
                    reportedPublication = (CognitivePart)cognitiveParts[publication_id];
                    break;
                case "Exercise":
                    reportedPublication = (Exercise)exercises[publication_id];
                    break;
                default:
                    reportedPublication = null;
                    break;
            }

            int? user_id = reader["CreatorID"] as int? ?? null;
            User? creator = user_id == null ? null : users[(int)user_id];

            return new Report(
                (int)reader["ID"],
                reader["Text"].ToString(),
                Convert.ToDateTime(reader["CreationDate"]),
                creator,
                reader["Reason"].ToString(),
                reportedPublication
                );
        }
    }
}
