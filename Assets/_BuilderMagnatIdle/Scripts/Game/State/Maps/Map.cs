using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities;
using ObservableCollections;
using R3;
using System.Linq;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Maps
{
    public class Map 
    {
        public ObservableList<Entity> Entities { get; } = new();

        public MapData Origin { get; }

        public int Id => Origin.Id;



        public Map(MapData mapData)
        {
            Origin = mapData;

            mapData.Entities.ForEach(entityData => Entities.Add(EntitiesFactory.CreatEntity(entityData)));
            
            Entities.ObserveAdd().Subscribe(e =>
            {
                var addedEntity = e.Value;
                mapData.Entities.Add(addedEntity.Origin);
            });
            
            Entities.ObserveRemove().Subscribe(e =>
            {
                var removedEntity = e.Value;
                var removedEntityData = mapData.Entities.FirstOrDefault(b => b.UniqueId == removedEntity.UniqueId);
                mapData.Entities.Remove(removedEntityData);
            });
        }
    }
}