using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjectsFactory
{
    class ObstaclesFactory : GameObjectFactory
    {
        public ObstaclesFactory(GameSettings settings) : base(settings)
        {

        }

        public override GameObject GetGameObject(Point objectPosition)
        {
            GameObject obstacle = new Obstacle() { ObjectDesign = GameSettings.obstacleDesign, Type = GameObjectType.Obstacle, Position = objectPosition };

            return obstacle;
        }

        public List<GameObject> GetObstacles()
        {
            List<GameObject> obstacles = new List<GameObject>();

            for (int i = 0; i < GameSettings.obstaclesCount; i++)
            {
                obstacles.Add(GetGameObject(GameSettings.GetRandomPoint()));
            }

            return obstacles;
        }
    }
}
