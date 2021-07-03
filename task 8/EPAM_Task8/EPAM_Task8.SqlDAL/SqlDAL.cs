using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace EPAM_Task8.SqlDAL
{
    public class SqlDAL
    {
        const string connectString = @"Data Source=DESKTOP-83KP24G;Initial Catalog=UsersAwardsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private SqlConnection _connection = new SqlConnection(connectString);

        public static void AddUser()
        {

        }

        public static void RemoveUser()
        {

        }

        public static void EditUser()
        {

        }


        public static void AddAward()
        {

        }

        public static void RemoveAward()
        {

        }

        public static void EditAward()
        {

        }


        public static void GiveAward()
        {

        }

        public static void TakeAwayAward()
        {

        }
    }
}
