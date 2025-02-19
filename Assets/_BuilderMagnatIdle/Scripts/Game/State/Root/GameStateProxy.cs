using Assets._BuilderMagnatIdle.Scripts.Game.State.GameResources;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Maps;
using ObservableCollections;
using R3;
using System.Linq;

namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        public ObservableList<Map> Maps { get; } = new();
        public ObservableList<Resource> Resources { get; } = new();

        public readonly ReactiveProperty<int> CurrentMapId = new();

        private readonly GameState gameState;

        public GameStateProxy(GameState gameState)
        {
            this.gameState = gameState;

            InitMaps(gameState);
            InitResources(gameState);

            CurrentMapId.Subscribe(newValue => { gameState.CurrentMapId = newValue; });
        }

        public int CreateEntityId()
        {
            return gameState.CreateEntityId();
        }

        private void InitMaps(GameState gameState)
        {
            gameState.Maps.ForEach(mapOrigin => Maps.Add(new Map(mapOrigin)));

            Maps.ObserveAdd().Subscribe(e =>
            {
                var addedMap = e.Value;
                gameState.Maps.Add(addedMap.Origin);
            });

            Maps.ObserveRemove().Subscribe(e =>
            {
                var removedMap = e.Value;
                var removedMapState = gameState.Maps.FirstOrDefault(b => b.Id == removedMap.Id);
                gameState.Maps.Remove(removedMapState);
            });

            
        }

        private void InitResources(GameState gameState)
        {
            gameState.Resources.ForEach(resourceData => Resources.Add(new Resource(resourceData)));

            Resources.ObserveAdd().Subscribe(e =>
            {
                var addedResource = e.Value;
                gameState.Resources.Add(addedResource.Origin);
            });

            Resources.ObserveRemove().Subscribe(e =>
            {
                var removedResource = e.Value;
                var removedResourceData = gameState.Resources.FirstOrDefault(b => b.ResourceType == removedResource.ResourceType);
                gameState.Resources.Remove(removedResourceData);
            });


        }
    }
}