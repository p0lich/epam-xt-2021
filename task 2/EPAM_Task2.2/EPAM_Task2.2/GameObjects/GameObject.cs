using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2.GameObjects
{
    public abstract class GameObject
    {
        private Point _position;
        private char _objectDesign;
        private GameObjectType _type;

        public Point Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public char ObjectDesign
        {
            get
            {
                return _objectDesign;
            }

            set
            {
                _objectDesign = value;
            }
        }

        public GameObjectType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
    }
}
