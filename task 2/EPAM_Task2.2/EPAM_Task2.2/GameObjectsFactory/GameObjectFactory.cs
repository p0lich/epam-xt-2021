using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjectsFactory
{
    abstract public class GameObjectFactory
    {
        public GameSettings GameSettings { get; set; }

        public abstract GameObject GetGameObject(Point objectPosition);

        public GameObjectFactory(GameSettings settings)
        {
            GameSettings = settings;
        }
    }
}
