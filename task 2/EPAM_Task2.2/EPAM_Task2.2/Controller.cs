using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM_Task2._2
{
    public class Controller
    {
        public event EventHandler pressLeftKey;
        public event EventHandler pressUpKey;
        public event EventHandler pressRightKey;
        public event EventHandler pressDownKey;

        public void StartListen()
        {
            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.Key.Equals(ConsoleKey.LeftArrow))
                {
                    pressLeftKey?.Invoke(this, new EventArgs());
                }

                else if (key.Key.Equals(ConsoleKey.UpArrow))
                {
                    pressUpKey?.Invoke(this, new EventArgs());
                }

                else if (key.Key.Equals(ConsoleKey.RightArrow))
                {
                    pressRightKey?.Invoke(this, new EventArgs());
                }

                else if (key.Key.Equals(ConsoleKey.DownArrow))
                {
                    pressDownKey?.Invoke(this, new EventArgs());
                }

                else
                {

                }
            }
        }
    }
}
