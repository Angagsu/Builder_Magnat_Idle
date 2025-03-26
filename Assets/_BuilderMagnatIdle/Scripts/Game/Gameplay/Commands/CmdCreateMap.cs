using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands
{
    public class CmdCreateMap : ICommand
    {
        public readonly int MapId;

        public CmdCreateMap(int mapId)
        {
            MapId = mapId;
        }
    }
}