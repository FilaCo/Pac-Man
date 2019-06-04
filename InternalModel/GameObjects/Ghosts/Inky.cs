using GameServer.GameMap;

namespace GameServer.GameObjects.Ghosts
{
    public class Inky: Ghost
    {
        private static readonly Inky Instance = new Inky();

        public static Inky GetInstance => Instance;
        
        public override MapPoint TargetPointScatter => new MapPoint(35, 27);

        public override MapPoint TargetPointChase
        {
            get
            {
                var pacManPos = PacMan.GetInstance.Position;
                var blinkyPos = Blinky.GetInstance.Position;

                var centerSegment = pacManPos[PacMan.GetInstance.Direction][PacMan.GetInstance.Direction];

                return centerSegment + (centerSegment - blinkyPos);
            }
        }

        private Inky()
        {
            
        }
}