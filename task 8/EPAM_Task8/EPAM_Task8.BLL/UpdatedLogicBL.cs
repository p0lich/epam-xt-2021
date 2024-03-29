﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM_Task8.SqlDAL;
using UpdatedEntity;

namespace EPAM_Task8.BLL
{
    public class UpdatedLogicBL
    {
        public static void CreateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException();
            }

            MySqlDAL.AddUser(user);
        }

        public static void CreateAward(Award award)
        {
            if (award is null)
            {
                throw new ArgumentNullException();
            }

            MySqlDAL.AddAward(award);
        }

        public static void GiveAward(int userId, int awardId)
        {
            MySqlDAL.GiveAward(userId, awardId);
        }

        public static List<Award> GetAwailableAwards()
        {
            return MySqlDAL.GetAllAwards();
        }

        public static List<User> GetAllUsers()
        {
            return MySqlDAL.GetAllUsers();
        }

        public static bool DeleteUser(int userId)
        {
            return MySqlDAL.RemoveUser(userId);
        }

        public static bool DeleteAward(int awardId)
        {
            return MySqlDAL.RemoveAward(awardId);
        }

        public static User GetUser(int userId)
        {
            return MySqlDAL.GetUser(userId);
        }

        public static User GetUser(string userName, string userPassword)
        {
            return MySqlDAL.GetUser(userName, userPassword);
        }

        public static Award GetAward(int awardId)
        {
            return MySqlDAL.GetAward(awardId);
        }

        public static string EncryptPassword()
        {
            return null;
        }

        public static List<Award> GetUserAwards(int userId)
        {
            return MySqlDAL.GetUserAwards(userId);
        }

        public static List<User> GetAwardUsers(int awardId)
        {
            return MySqlDAL.GetAwardUsers(awardId);
        }

        public static bool TakeUserAward(int userId, int awardId)
        {
            return MySqlDAL.TakeUserAward(userId, awardId);
        }

        public static bool UpdateUser(User user)
        {
            return MySqlDAL.EditUser(user);
        }

        public static bool UpdateAward(Award award)
        {
            return MySqlDAL.EditAward(award);
        }
    }
}
