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
    public class ReviewConverter : IDTOConverter
    {
        private Dictionary<int, User> users;
        public ReviewConverter(Dictionary<int, User> users)
        {
            this.users = users;
        }
        public Publication ConvertFromDTO(SqlDataReader reader)
        {
            return new Review(
                (int)reader["ID"],
                reader["Text"].ToString(),
                Convert.ToDateTime(reader["CreationDate"]),
                users[Convert.ToInt32(reader["CreatorID"])],
                Convert.ToInt32(reader["Ranking"])
                );
        }
    }
}
