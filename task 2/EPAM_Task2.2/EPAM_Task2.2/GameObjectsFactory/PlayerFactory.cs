using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjectsFactory
{
    public class PlayerFactory : GameObjectFactory
    {
        public PlayerFactory(GameSettings settings) : base(settings)
        {

        }

        public override GameObject GetGameObject(Point objectPosition)
        {
            GameObject player = new Player() { ObjectDesign = GameSettings.playerDesign, Type = GameObjectType.Player, Position = objectPosition };

            return player;
        }

        public GameObject GetGameObject()
        {
            Point position = GetPlayerPosition();
            return GetGameObject(position);
        }

        private Point GetPlayerPosition()
        {
            return GameSettings.GetRandomPoint();
        }
    }
}
