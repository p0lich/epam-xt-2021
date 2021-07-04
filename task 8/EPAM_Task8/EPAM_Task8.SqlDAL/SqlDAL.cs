using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using UpdatedEntity;

namespace EPAM_Task8.SqlDAL
{
    public static class MySqlDAL
    {
        const string connectString = @"Data Source=DESKTOP-83KP24G;Initial Catalog=UsersAwardsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlConnection _connection;

        #region USER_TABLE_OPERATIONS

        public static bool AddUser(User user)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.CreateUser";
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@password", user.GetPassword());
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                _connection.Open();

                var result = command.ExecuteScalar();

                if (result == null)
                {
                    throw new InvalidOperationException(string.Format("Something go wrong during user creation"));
                }

                return true;
            }
        }

        public static bool RemoveUser(int userId)
        {
            return RemoveNote(userId, "dbo.RemoveUser");
        }

        public static bool EditUser(User user)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.ChangeUser";
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@password", user.GetPassword());
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                _connection.Open();

                var result = command.ExecuteScalar();

                //if (result == null)
                //{
                //    throw new InvalidOperationException(string.Format("Something go wrong during user creation"));
                //}

                return true;
            }
        }

        public static User GetUser(int userId)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.User_GetById";

                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", userId);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader == null)
                {
                    throw new ArgumentNullException(string.Format("User not found"));
                }

                return new User(
                    id: (int)reader["id"],
                    isAdmin: (bool)reader["IsAdmin"],
                    dateOfBirth: (DateTime)reader["DateOfBirth"],
                    name: reader["Name"] as string,
                    password: reader["Password"] as string
                    );
            }
        }

        public static User GetUser(string name, string password)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.User_GetByNameAndPassword";

                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@password", password);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader == null)
                {
                    throw new ArgumentNullException(string.Format("Cant find user with such name and password"));
                }

                return new User(
                    id: (int)reader["id"],
                    isAdmin: (bool)reader["IsAdmin"],
                    dateOfBirth: (DateTime)reader["DateOfBirth"],
                    name: reader["Name"] as string,
                    password: reader["Password"] as string
                    );
            }
        }

        public static List<User> GetAllUsers()
        {
            using (_connection = new SqlConnection(connectString))
            {
                string query = "SELECT Id, Name, DateOfBirth, IsAdmin, Password FROM Users";
                SqlCommand command = new SqlCommand(query, _connection);

                _connection.Open();

                var reader = command.ExecuteReader();

                List<User> users = new List<User>();

                while (reader.Read())
                {
                    users.Add(new User(
                        id: (int)reader["id"],
                        isAdmin: (bool)reader["IsAdmin"],
                        dateOfBirth: (DateTime)reader["DateOfBirth"],
                        name: reader["Name"] as string,
                        password: reader["Password"] as string
                        ));
                }

                return users;
            }
        }

        #endregion

        #region AWARD_TABLE_OPERATIONS

        public static bool AddAward(Award award)
        {
            using (_connection = new SqlConnection(connectString))
            {
                if (!IsAwardUnique(award.Title))
                {
                    throw new ArgumentException(string.Format("Award with title {0} already exist", award.Title));
                }

                string strProc = "dbo.CreateAward";
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue(@"title", award.Title);

                _connection.Open();

                var result = command.ExecuteScalar();

                if (result == null)
                {
                    throw new InvalidOperationException(string.Format("Something go wrong during user creation"));
                }

                return true;
            }
        }

        public static bool RemoveAward(int awardId)
        {
            return RemoveNote(awardId, "dbo.RemoveAward");
        }

        public static bool EditAward(Award award)
        {
            using (_connection = new SqlConnection(connectString))
            {
                if (!IsAwardUnique(award.Title))
                {
                    throw new ArgumentException(string.Format("Award with title {0} already exist", award.Title));
                }

                string strProc = "dbo.ChangeAward";
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue(@"id", award.Id);
                command.Parameters.AddWithValue(@"title", award.Title);

                _connection.Open();

                var result = command.ExecuteScalar();

                //if (result == null)
                //{
                //    throw new InvalidOperationException(string.Format("Something go wrong during user creation"));
                //}

                return true;
            }
        }

        public static Award GetAward(int awardId)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.Award_GetById";

                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", awardId);

                _connection.Open();

                var reader = command.ExecuteReader();

                return new Award(
                    id: (int)reader["id"],
                    title: reader["Title"] as string
                    );
            }
        }

        public static List<Award> GetAllAwards()
        {
            using (_connection = new SqlConnection(connectString))
            {
                string query = "SELECT Id, Title FROM Awards";
                SqlCommand command = new SqlCommand(query, _connection);

                _connection.Open();

                var reader = command.ExecuteReader();

                List<Award> awards = new List<Award>();

                while (reader.Read())
                {
                    awards.Add(new Award(
                        id: (int)reader["id"],
                        title: reader["Title"] as string
                        ));
                }

                return awards;
            }
        }

        #endregion


        public static bool GiveAward(int userId, int awardId)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.UserAward_AddNote";
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue(@"id_user", userId);
                command.Parameters.AddWithValue(@"id_award", awardId);

                _connection.Open();

                var result = command.ExecuteScalar();

                return true;
            }
        }

        public static bool TakeAwayFromAllUsers(int awardId)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string strProc = "dbo.UserAward_RemoveAward";
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue(@"id_award", awardId);

                _connection.Open();

                var result = command.ExecuteScalar();

                return true;
            }
        }

        private static bool RemoveNote(int id, string strProc)
        {
            using (_connection = new SqlConnection(connectString))
            {
                SqlCommand command = new SqlCommand(strProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", id);

                _connection.Open();

                var result = command.ExecuteScalar();

                // TODO: check result

                return true;
            }
        }

        public static List<Award> GetUserAwards(int userId)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string stProc = "dbo.User_GetAwards";

                SqlCommand command = new SqlCommand(stProc, _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                

                command.Parameters.AddWithValue("@id", userId);

                _connection.Open();

                var reader = command.ExecuteReader();

                List<Award> userAwards = new List<Award>();

                while (reader.Read())
                {
                    int a = (int)reader["Id"];
                    string b = reader["Title"] as string;

                    userAwards.Add(new Award(
                        id: (int)reader["Id"],
                        title: reader["Title"] as string
                        ));
                }

                return userAwards;
            }
        }

        public static List<User> GetAwardUsers(int awardId)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string query = "dbo.Award_GetUsers";

                SqlCommand command = new SqlCommand(query, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@id", awardId);

                _connection.Open();

                var reader = command.ExecuteReader();

                List<User> awardUsers = new List<User>();

                while (reader.Read())
                {
                    awardUsers.Add(new User(
                        id: (int)reader["id"],
                        isAdmin: (bool)reader["IsAdmin"],
                        dateOfBirth: (DateTime)reader["DateOfBirth"],
                        name: reader["Name"] as string,
                        password: reader["Password"] as string
                        ));
                }

                return awardUsers;
            }
        }

        private static bool IsAwardUnique(string awardTitle)
        {
            using (_connection = new SqlConnection(connectString))
            {
                string query = "SELECT Title FROM Awards";
                SqlCommand command = new SqlCommand(query, _connection);

                _connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (string.Equals(reader["Title"] as string, awardTitle))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
