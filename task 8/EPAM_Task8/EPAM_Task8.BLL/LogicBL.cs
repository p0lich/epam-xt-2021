using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM_Task8.DAL;
using EPAM_Task8.Entities;
//using UpdatedEntity;

namespace EPAM_Task8.BLL
{
    public class LogicBL
    {
        public static void CreateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException();
            }

            NoteJsonDal.AddUser(user);
        }

        public static void CreateAward(Award award)
        {
            if (award is null)
            {
                throw new ArgumentNullException();
            }

            NoteJsonDal.AddAward(award);
        }

        public static void GiveAward(User user, Award award)
        {
            NoteJsonDal.GiveAward(user.Id, award.Id);
        }

        public static List<Award> GetAwailableAwards()
        {
            return NoteJsonDal.GetAwailableAwards();
        }

        public static List<User> GetAllUsers()
        {
            return NoteJsonDal.GetAllUsers();
        }

        public static bool DeleteUser(Guid userId)
        {
            return NoteJsonDal.RemoveUser(userId);
        }

        public static void FillAwardForm(Award award, string fieldName)
        {

        }

        public static string EncryptPassword()
        {
            return null;
        }
    }
}
