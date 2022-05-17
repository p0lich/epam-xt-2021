using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using EPAM_Task8.Entities;

namespace EPAM_Task8.DAL
{
    public static class NoteJsonDal
    {
        // temp path
        private const string JsonDataPath = @"C:\Users\Alex\Documents\epam-xt-2021\task 8\EPAM_Task8\JsonData\";

        private const string UsersJsonPath = JsonDataPath + @"UsersJson\";
        private const string AwardsJsonPath = JsonDataPath + @"AwardsJson\";
        private const string UserAwardCompositeJsonPath = JsonDataPath + @"UserAwardComposite\";

        public static void AddUser(User user)
        {
            string jsonPath = UsersJsonPath + user.Id + ".json";
            string jsonData = JsonConvert.SerializeObject(user);

            SaveData(jsonPath, jsonData);
        }

        public static bool RemoveUser(Guid userId)
        {
            List<Award> availableAwards = GetAwailableAwards();

            for (int i = 0; i < availableAwards.Count; i++)
            {
                if (availableAwards[i].UsersId.Contains(userId))
                {
                    availableAwards[i].UsersId.Remove(userId);

                    string jsonAwardPath = AwardsJsonPath + availableAwards[i].Id + ".json";
                    string jsonAwardData = JsonConvert.SerializeObject(availableAwards[i]);

                    SaveData(jsonAwardPath, jsonAwardData);
                }
            }

            return DeleteData(UsersJsonPath + userId + ".json");
        }

        public static void EditUser(Guid id, User user)
        {
            if (!File.Exists(UsersJsonPath + id + ".json"))
            {
                throw new FileNotFoundException();
            }

            string jsonPath = UsersJsonPath + id + ".json";
            User updatedUser = new User(id, user);
            string newJsonData = JsonConvert.SerializeObject(updatedUser);

            SaveData(jsonPath, newJsonData);
        }



        public static void AddAward(Award award)
        {
            string jsonPath = AwardsJsonPath + award.Id + ".json";
            string jsonData = JsonConvert.SerializeObject(award);

            SaveData(jsonPath, jsonData);
        }

        public static bool RemoveAward(Guid awardId)
        {
            List<User> users = GetAllUsers();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].AwardsId.Contains(awardId))
                {
                    users[i].AwardsId.Remove(awardId);

                    string jsonUserPath = UsersJsonPath + users[i].Id + ".json";
                    string jsonUserData = JsonConvert.SerializeObject(users[i]);

                    SaveData(jsonUserPath, jsonUserData);
                }
            }

            return DeleteData(AwardsJsonPath + awardId + ".json");
        }

        public static void EditAward(Guid Id)
        {

        }



        public static bool GiveAward(Guid userId, Guid awardId)
        {
            try
            {
                string userData = File.ReadAllText(UsersJsonPath + userId + ".json");
                User user = JsonConvert.DeserializeObject<User>(userData);

                string awardData = File.ReadAllText(AwardsJsonPath + awardId + ".json");
                Award award = JsonConvert.DeserializeObject<Award>(awardData);

                if (user.AwardsId.Contains(award.Id))
                {
                    return false;
                }

                user.GetAward(award);
                award.AddUser(user);

                string jsonUserPath = UsersJsonPath + userId + ".json";
                string jsonUserData = JsonConvert.SerializeObject(user);

                string jsonAwardPath = AwardsJsonPath + awardId + ".json";
                string jsonAwardData = JsonConvert.SerializeObject(award);

                SaveData(jsonUserPath, jsonUserData);
                SaveData(jsonAwardPath, jsonAwardData);

                return true;
            }

            catch
            {
                return false;
            }           
        }

        public static void TakeAwayAward()
        {

        }

        //public static void UpdateUserAwards(Guid userId, Guid awardId)
        //{
        //    string userData = File.ReadAllText(UsersJsonPath + userId + ".json");
        //    User user = JsonConvert.DeserializeObject<User>(userData);

        //    string awardData = File.ReadAllText(AwardsJsonPath + awardId + ".json");
        //    Award award = JsonConvert.DeserializeObject<Award>(awardData);

        //    user.GetAward(award);

        //    string jsonPath = UsersJsonPath + userId + ".json";
        //    string jsonData = JsonConvert.SerializeObject(user);

        //    SaveData(jsonPath, jsonData);
        //}

        public static List<Award> GetAwailableAwards()
        {
            try
            {
                DirectoryInfo awardsDirecroty = new DirectoryInfo(AwardsJsonPath);
                FileInfo[] awardsFiles = awardsDirecroty.GetFiles("*.json");

                List<Award> awards = new List<Award>();

                for (int i = 0; i < awardsFiles.Length; i++)
                {
                    string jsonData = File.ReadAllText(awardsFiles[i].FullName);
                    awards.Add(JsonConvert.DeserializeObject<Award>(jsonData));
                }

                return awards;
            }

            catch
            {
                // do smth
                return null;
            }
        }

        public static List<User> GetAllUsers()
        {
            try
            {
                DirectoryInfo usersDirecroty = new DirectoryInfo(UsersJsonPath);
                FileInfo[] usersFiles = usersDirecroty.GetFiles("*.json");

                List<User> users = new List<User>();

                for (int i = 0; i < usersFiles.Length; i++)
                {
                    string jsonData = File.ReadAllText(usersFiles[i].FullName);
                    users.Add(JsonConvert.DeserializeObject<User>(jsonData));
                }

                return users;
            }

            catch
            {
                // do smth
                return null;
            }
        }

        private static void SaveData(string filePath, string fileData)
        {
            try
            {
                using (FileStream fs = File.Create(filePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(fileData);
                    fs.Write(info, 0, info.Length);
                }
            }

            catch
            {
                // do smth
                return;
            }
            
        }

        private static bool DeleteData(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
