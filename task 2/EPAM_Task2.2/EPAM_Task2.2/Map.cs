using EPAM_Task2._2.GameObjects;
using EPAM_Task2._2.GameObjectsFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2
{
    public enum GameObjectType
    {
        EmptyPlace,
        Obstacle,
        MapBound,
        Bonus,
        Enemy,
        Player
    }

    public struct Point
    {
        private int _x, _y;

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                if (value >= 0)
                {
                    _x = value;
                }
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                if (value >= 0)
                {
                    _y = value;
                }
            }
        }

        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public static bool operator ==(Point p1, Point p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            if (p1.X != p2.X || p1.Y != p2.Y)
            {
                return true;
            }

            return false;
        }
    }

    public class Map
    {
        private List<GameObject> _enemies;
        private List<GameObject> _obstacles;
        private List<GameObject> _bonuses;
        private List<GameObject> _bounds;
        private GameObject _player;
        private GameSettings _settings;
        private int _score;
        private int _playerHp;

        private static Map _map;

        public List<GameObject> Enemies
        {
            get
            {
                return _enemies;
            }

            set
            {
                _enemies = value;
            }
        }

        public List<GameObject> Obstacles
        {
            get
            {
                return _obstacles;
            }

            set
            {
                _obstacles = value;
            }
        }

        public List<GameObject> Bonuses
        {
            get
            {
                return _bonuses;
            }

            set
            {
                _bonuses = value;
            }
        }

        public List<GameObject> Bounds
        {
            get
            {
                return _bounds;
            }

            set
            {
                _bounds = value;
            }
        }

        public GameObject Player
        {
            get
            {
                return _player;
            }

            set
            {
                _player = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
            }
        }

        public int PlayerHp
        {
            get
            {
                return _playerHp;
            }

            set
            {
                _playerHp = value;
            }
        }

        private Map() { }

        private Map(GameSettings settings)
        {
            _settings = settings;
            _enemies = new EnemiesFactory(_settings).GetEnemies();
            _obstacles = new ObstaclesFactory(_settings).GetObstacles();
            _player = new PlayerFactory(_settings).GetGameObject();
            _bonuses = new BonusesFactory(_settings).GetBonuses();
            _bounds = new BoundsFactory(_settings).GetBounds();
            _score = 0;
            _playerHp = 3;
        }

        public static Map GetMap(GameSettings settings)
        {
            if(_map == null)
            {
                _map = new Map(settings);
            }

            return _map;
        }
    }
}
