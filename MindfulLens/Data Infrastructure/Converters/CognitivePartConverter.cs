using Logic_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Data_Infrastructure.Converters
{
    public class CognitivePartConverter : IDTOConverter
    {
        private Dictionary<int, User> users;

        public CognitivePartConverter(Dictionary<int, User> users)
        {
            this.users = users;
        }

        public Publication ConvertFromDTO(SqlDataReader reader)
        {
            return new CognitivePart(
                (int)reader["ID"],
                reader["Text"].ToString(),
                Convert.ToDateTime(reader["CreationDate"]),
                users[Convert.ToInt32(reader["CreatorID"])],
                reader["Title"].ToString(),
                reader["ClassName"].ToString(),
                reader["WayOfHandling"].ToString(),
                Convert.ToInt32(reader["IsOfficial"]) == 1
            );
        }
    }
}
