using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjectsFactory
{
    class BoundsFactory : GameObjectFactory
    {
        public BoundsFactory(GameSettings settings) : base(settings)
        {

        }

        public override GameObject GetGameObject(Point objectPosition)
        {
            GameObject bound = new Bound() { ObjectDesign = GameSettings.boundDesign, Type = GameObjectType.MapBound, Position = objectPosition };

            return bound;
        }

        public List<GameObject> GetBounds()
        {
            List<GameObject> bounds = new List<GameObject>();

            for (int y = 0; y < GameSettings.mapHeight; y++)
            {
                if (y == 0 || y == GameSettings.mapHeight - 1)
                {
                    for (int x = 0; x < GameSettings.mapWidth; x++)
                    {
                        bounds.Add(GetGameObject(new Point(x, y)));
                    }
                }

                else
                {
                    bounds.Add(GetGameObject(new Point(0, y)));
                    bounds.Add(GetGameObject(new Point(GameSettings.mapWidth - 1, y)));
                }
            }

            return bounds;
        }
    }
}
