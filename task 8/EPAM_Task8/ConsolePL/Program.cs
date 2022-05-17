using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM_Task8.BLL;
using EPAM_Task8.Entities;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            //User testUser1 = new User("Roma", DateTime.Now, 20);
            //Guid Id = testUser1.Id;

            //LogicBL.CreateUser(testUser1);
            //LogicBL.DeleteUser(Id);

            //Award testAward1 = new Award("best");
            //Award testAward2 = new Award("beautiful");
            //Award testAward3 = new Award("gorgeous");

            //LogicBL.CreateAward(testAward1);
            //LogicBL.CreateAward(testAward2);
            //LogicBL.CreateAward(testAward3);

            //List<Award> awards = LogicBL.GetAwailableAwards();

            User testUser2 = new User("Ivan", DateTime.Now, 20);
            LogicBL.CreateUser(testUser2);

            Award testAward4 = new Award("powerfull");
            LogicBL.CreateAward(testAward4);

            LogicBL.GiveAward(testUser2, testAward4);

            Console.ReadKey();
        }
    }
}
