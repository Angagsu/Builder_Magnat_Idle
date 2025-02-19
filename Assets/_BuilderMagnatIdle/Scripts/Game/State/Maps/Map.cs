using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using ObservableCollections;
using R3;
using System.Linq;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Maps
{
    public class Map 
    {
        public ObservableList<BuildingEntityProxy> Buildings { get; } = new();

        public MapState Origin { get; }

        public int Id => Origin.Id;



        public Map(MapState mapState)
        {
            Origin = mapState;

            mapState.Buildings.ForEach(buidingOrigin => Buildings.Add(new BuildingEntityProxy(buidingOrigin)));

            Buildings.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingEntity = e.Value;
                mapState.Buildings.Add(addedBuildingEntity.Origin);
            });

            Buildings.ObserveRemove().Subscribe(e =>
            {
                var removedBuildingEntityProxy = e.Value;
                var removedBuildingEntity = mapState.Buildings.FirstOrDefault(b => b.Id == removedBuildingEntityProxy.Id);
                mapState.Buildings.Remove(removedBuildingEntity);
            });
        }
    }
}