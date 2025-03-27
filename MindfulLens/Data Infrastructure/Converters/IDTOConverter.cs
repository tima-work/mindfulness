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
    public interface IDTOConverter
    {
        Publication ConvertFromDTO(SqlDataReader reader);
    }
}
