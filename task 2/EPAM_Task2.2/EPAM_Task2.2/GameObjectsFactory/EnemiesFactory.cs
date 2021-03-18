using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjectsFactory
{
    class EnemiesFactory : GameObjectFactory
    {
        public EnemiesFactory(GameSettings settings) : base(settings)
        {

        }

        public override GameObject GetGameObject(Point objectPosition)
        {
            GameObject enemy = new Enemy() {ObjectDesign = GameSettings.enemyDesign, Type = GameObjectType.Enemy, Position = objectPosition};

            return enemy;
        }

        public List<GameObject> GetEnemies()
        {
            List<GameObject> enemies = new List<GameObject>();       

            for (int i = 0; i < GameSettings.enemiesCount; i++)
            {
                enemies.Add(GetGameObject(GameSettings.GetRandomPoint()));
            }

            return enemies;
        }
    }
}
