using System;
using System.Threading.Tasks;
using GameServer;
using GameServer.GameMap;
using GameServer.GameObjects;

namespace MainWindow
{
    public class GameModel
    {
        private  const float MaxValueAccum = 0.2f;

        private IActorsChecker _checker;
        
        public IScene Scene { get; }
        
        public IGameWindow Window { get; set; }

        public float DeltaTime { get; set; }

        public GameModel(IScene scene, IGameWindow window, float fps = 60)
        {
            Window = window;
            Scene = scene;
            DeltaTime = 1 / fps;
            _checker = new ActorsChecker();
        }

        public void Execute()
        {
            var accumulator = 0.0f;
            var frameStartTime = DateTime.Now;

            while (true)
            {
                var elapsedSeconds = (DateTime.Now - frameStartTime).TotalSeconds;

                frameStartTime = DateTime.Now;

                accumulator += (float) elapsedSeconds;

                if (accumulator > MaxValueAccum)
                    accumulator = MaxValueAccum;

                while (accumulator >= DeltaTime)
                {
                    Scene.Update();
                    _checker.Check();

                    accumulator -= DeltaTime;
                }

                Window.RenderGameFrame(accumulator / DeltaTime);
            }
        }

        public async Task ExecuteAsync() => await Task.Run(Execute);
    }
}