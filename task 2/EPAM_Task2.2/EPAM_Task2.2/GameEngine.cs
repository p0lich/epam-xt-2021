using EPAM_Task2._2.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EPAM_Task2._2
{
    class GameEngine
    {
        private bool _isRun, _isWin;
        private static GameEngine _gameEngine;
        private Map _map;
        private MapRender _mapRender;

        private GameEngine() { }

        private GameEngine(GameSettings settings)
        {
            _isRun = true;
            _isWin = false;
            _map = Map.GetMap(settings);
            _mapRender = new MapRender();
        }

        public static GameEngine GetGameEngine(GameSettings settings)
        {
            if (_gameEngine == null)
            {
                _gameEngine = new GameEngine(settings);
            }

            return _gameEngine;
        }

        public void Run()
        {
            do
            {
                _mapRender.Render(_map);
                CalculateEnemyMove();

                Thread.Sleep(100);
                Console.Clear();
            } while (_isRun);

            Console.Clear();
            _mapRender.RenderEndScreen(_isWin, _map);
        }

        public void CalculatePlayerMoveLeft()
        {
            GameObject player = _map.Player;
            Point displace = new Point(player.Position.X - 1, player.Position.Y);
            MakePlayerMove(displace);
        }

        public void CalculatePlayerMoveUp()
        {
            GameObject player = _map.Player;
            Point displace = new Point(player.Position.X, player.Position.Y - 1);
            MakePlayerMove(displace);
        }

        public void CalculatePlayerMoveright()
        {
            GameObject player = _map.Player;
            Point displace = new Point(player.Position.X + 1, player.Position.Y);
            MakePlayerMove(displace);
        }

        public void CalculatePlayerMoveDown()
        {
            GameObject player = _map.Player;
            Point displace = new Point(player.Position.X, player.Position.Y + 1);
            MakePlayerMove(displace);
        }

        public void CalculateEnemyMove()
        {
            for (int i = 0; i < _map.Enemies.Count; i++)
            {
                GameObject enemy = _map.Enemies[i];

                Point displace = new Point(enemy.Position.X - 1, enemy.Position.Y);

                GameObjectType displaceType = CheckCurrentObject(displace);
                bool isEmpty = false;

                do
                {
                    switch (displaceType)
                    {
                        case GameObjectType.Obstacle:
                            displace = new Point(displace.X - 1, displace.Y);
                            displaceType = CheckCurrentObject(displace);
                            break;

                        case GameObjectType.MapBound:
                            displace = new Point(GameSettings.mapWidth - 2, displace.Y);
                            displaceType = CheckCurrentObject(displace);
                            break;

                        case GameObjectType.Player:
                            //_map.PlayerHp--;
                            //_map.Score -= 50;

                            //GamefinishCheck();

                            _map.Enemies.RemoveAt(i);
                            i--;
                            isEmpty = true;
                            break;

                        default:
                            isEmpty = true;
                            break;
                    }
                } while (!isEmpty);

                enemy.Position = displace;
                //_map.Enemies[i] = enemy;
            }
        }

        private GameObjectType CheckCurrentObject(Point point)
        {
            foreach (var bound in _map.Bounds)
            {
                if (point == bound.Position)
                {
                    return GameObjectType.MapBound;
                }
            }

            foreach (var obstacle in _map.Obstacles)
            {
                if (point == obstacle.Position)
                {
                    return GameObjectType.Obstacle;
                }
            }

            foreach (var bonus in _map.Bonuses)
            {
                if (point == bonus.Position)
                {
                    return GameObjectType.Bonus;
                }
            }

            foreach (var enemy in _map.Enemies)
            {
                if (point == enemy.Position)
                {
                    return GameObjectType.Enemy;
                }
            }

            return GameObjectType.EmptyPlace;
        }

        private void MakePlayerMove(Point displace)
        {
            GameObjectType displaceType = CheckCurrentObject(displace);

            switch (displaceType)
            {
                case GameObjectType.Obstacle:
                    return;

                case GameObjectType.MapBound:
                    return;

                case GameObjectType.Bonus:
                    _map.Score += 100;
                    RemoveObject(_map.Bonuses, displace);
                    GamefinishCheck();

                    break;

                case GameObjectType.Enemy:
                    _map.PlayerHp--;
                    _map.Score -= 50;

                    GamefinishCheck();

                    break;

                default:
                    break;
            }

            _map.Player.Position = displace;
        }

        private void RemoveObject(List<GameObject> gameObjects, Point position)
        {
            gameObjects.RemoveAt(FindObjectIndex(gameObjects, position));
        }

        private int FindObjectIndex(List<GameObject> gameObjects, Point position)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].Position == position)
                {
                    return i;
                }
            }

            return -1;
        }

        private void GamefinishCheck()
        {
            if (_map.PlayerHp == 0)
            {
                _isRun = false;
                return;
            }

            if (_map.Bonuses.Count == 0)
            {
                _isRun = false;
                _isWin = true;
                return;
            }
        }
    }
}
