using System;
using System.Collections.Generic;
using System.Threading;

namespace EPAM_Task2._2
{
    class Item
    {

    }

    class Program
    {
        static GameEngine engine;
        static GameSettings settings;
        static Controller controller;

        static void Main(string[] args)
        {
            //Map map = Map.GetMap(new GameSettings());
            //MapRender game = new MapRender();
            //game.Render(map);

            Initialize();
            engine.Run();
        }

        public static void Initialize()
        {
            settings = new GameSettings();

            engine = GameEngine.GetGameEngine(settings);

            controller = new Controller();

            controller.pressLeftKey += (obj, arg) => engine.CalculatePlayerMoveLeft();
            controller.pressUpKey += (obj, arg) => engine.CalculatePlayerMoveUp();
            controller.pressRightKey += (obj, arg) => engine.CalculatePlayerMoveright();
            controller.pressDownKey += (obj, arg) => engine.CalculatePlayerMoveDown();

            Thread uIthread = new Thread(controller.StartListen);
            uIthread.Start();
        }
    }
}