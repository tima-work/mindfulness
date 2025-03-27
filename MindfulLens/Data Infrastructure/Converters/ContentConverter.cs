using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Converters
{
    public class ContentConverter : IDTOConverter
    {
        private Dictionary<int, User> users;

        public ContentConverter(Dictionary<int, User> users)
        {
            this.users = users;
        }

        public Publication ConvertFromDTO(SqlDataReader reader)
        {
            return new Content(
                (int)reader["ID"],
                reader["Text"].ToString(),
                Convert.ToDateTime(reader["CreationDate"]),
                users[Convert.ToInt32(reader["CreatorID"])],
                reader["Title"].ToString(),
                reader["ClassName"].ToString(),
                reader["Photo"] as byte[]);
        }
    }
}
