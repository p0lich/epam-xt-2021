using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjectsFactory
{
    class BonusesFactory : GameObjectFactory
    {
        public BonusesFactory(GameSettings settings) : base(settings)
        {

        }

        public override GameObject GetGameObject(Point objectPosition)
        {
            GameObject bonus = new Enemy() { ObjectDesign = GameSettings.bonusDesign, Type = GameObjectType.Bonus, Position = objectPosition };

            return bonus;
        }

        public List<GameObject> GetBonuses()
        {
            List<GameObject> bonuses = new List<GameObject>();

            for (int i = 0; i < GameSettings.bonusesCount; i++)
            {
                bonuses.Add(GetGameObject(GameSettings.GetRandomPoint()));
            }

            return bonuses;
        }
    }
}
