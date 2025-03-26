using Assets._BuilderMagnatIdle.Scripts.Game.State.GameResources;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Maps;
using System.Collections.Generic;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Root
{
    public class GameState 
    {
        public int GlobalEntityId { get; set; }
        public int CurrentMapId { get; set; }
        public List<MapData> Maps { get; set; }
        public List<ResourceData> Resources { get; set; }

        public int CreateEntityId()
        {
            return GlobalEntityId++;
        }
    }
}