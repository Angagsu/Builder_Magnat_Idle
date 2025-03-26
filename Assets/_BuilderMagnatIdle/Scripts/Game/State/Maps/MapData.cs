using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities;
using System.Collections.Generic;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Maps
{
    public class MapData
    {
        public int Id { get; set; }
        public List<EntityData> Entities { get; set; }
    }
}