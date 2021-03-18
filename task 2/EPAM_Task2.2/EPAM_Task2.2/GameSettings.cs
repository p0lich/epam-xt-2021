using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2
{
    public class GameSettings
    {
        public const int enemiesCount = 5;
        public const int obstaclesCount = 10;
        public const int bonusesCount = 5;

        public const int mapWidth = 40;
        public const int mapHeight = 20;

        // Designs
        public const char playerDesign = 'H';
        public const char enemyDesign = '<';
        public const char bonusDesign = 'O';
        public const char obstacleDesign = '¤';
        public const char emptyFieldDesign = ' ';
        public const char boundDesign = '*';

        private Random _rand;
        private static bool[,] _filledPoints = new bool[mapHeight, mapWidth];

        public Point GetRandomPoint()
        {
            _rand = new Random();
            while (true)
            {
                int x = _rand.Next(1, mapWidth - 1);
                int y = _rand.Next(1, mapHeight - 1);

                if (!_filledPoints[y, x])
                {
                    _filledPoints[y, x] = true;
                    return new Point(x, y);
                }
            }    
        }
    }
}
