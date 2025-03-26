using UnityEngine;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Entities
{
    public class EntityData 
    {
        public int UniqueId { get; set; }
        public string ConfigId { get; set; }
        public  EntityType Type { get; set; }
        public Vector2Int Position { get; set; }
    }
}