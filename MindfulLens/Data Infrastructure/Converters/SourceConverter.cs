using Dapper;
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
    public class SourceConverter : IDTOConverter
    {
        private Dictionary<int, User> users;
        private Dictionary<int, List<string>> links;
        public SourceConverter(Dictionary<int, User> users, Dictionary<int, List<string>> links)
        {
            this.users = users;
            this.links = links; 
        }

        public Publication ConvertFromDTO(SqlDataReader reader)
        {
            int source_id = (int)reader["ID"];
            List<string> linksList = links.ContainsKey(source_id) ? links[source_id] : new List<string>();
            return new Source(
                source_id,
                reader["Text"].ToString(),
                Convert.ToDateTime(reader["CreationDate"]),
                users[Convert.ToInt32(reader["CreatorID"])],
                reader["Title"].ToString(),
                linksList.ToArray(),
                reader["Author"].ToString(),
                Convert.ToInt32(reader["IsOfficial"]) == 1
            );
        }
    }
}
