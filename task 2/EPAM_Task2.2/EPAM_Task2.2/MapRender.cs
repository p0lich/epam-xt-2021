using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2
{
    public class MapRender
    {
        private int _screenWidth, _screenHeight;

        private char[,] _screenMatrix;

        public MapRender()
        {
            _screenWidth = GameSettings.mapWidth;
            _screenHeight = GameSettings.mapHeight;
            _screenMatrix = new char[_screenHeight, _screenWidth];
        }

        public void Render(Map map)
        {
            FillMatrix(map);

            StringBuilder str = new StringBuilder();

            str.Append(String.Format("HP: {0}\n", map.PlayerHp));
            str.Append(String.Format("Score: {0}\n\n", map.Score));

            for (int i = 0; i < _screenHeight; i++)
            {
                for (int k = 0; k < _screenWidth; k++)
                {
                    str.Append(_screenMatrix[i, k]);
                }

                str.Append(Environment.NewLine);
            }

            Console.WriteLine(str.ToString());
        }

        public void RenderEndScreen(bool isWin, Map map)
        {
            StringBuilder endGameInfo = new StringBuilder("Game over\n");

            if (isWin)
            {
                endGameInfo.Append("You won\n");
            }

            else
            {
                endGameInfo.Append("You loose\n");
            }

            endGameInfo.Append(String.Format("Score: {0}", map.Score));

            Console.WriteLine(endGameInfo.ToString());
        }

        public void ClearScreen()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < _screenHeight; i++)
            {
                for (int k = 0; k < _screenWidth; k++)
                {
                    str.Append(' ');
                }

                str.Append(Environment.NewLine);
            }

            Console.WriteLine(str.ToString());
        }

        private void FillMatrix(Map map)
        {
            for (int i = 0; i < _screenMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < _screenMatrix.GetLength(1); k++)
                {
                    _screenMatrix[i, k] = GameSettings.emptyFieldDesign;
                }
            }

            AddList(map.Bounds);          
            AddList(map.Obstacles);           
            AddList(map.Bonuses);
            AddList(map.Enemies);
            AddGameObject(map.Player);
        }

        private void AddGameObject(GameObject gameObject)
        {
            _screenMatrix[gameObject.Position.Y, gameObject.Position.X] = gameObject.ObjectDesign;
        }

        private void AddList(List<GameObject> gameObjects)
        {
            foreach (var item in gameObjects)
            {
                AddGameObject(item);
            }
        }
    }
}
