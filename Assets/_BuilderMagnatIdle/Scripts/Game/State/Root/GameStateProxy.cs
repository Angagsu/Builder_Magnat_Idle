﻿using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using ObservableCollections;
using R3;
using System.Linq;

namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        public ObservableList<BuildingEntityProxy> Buildings { get; } = new();


        public GameStateProxy(GameState gameState)
        {
            gameState.Buildings.ForEach(buidingOrigin => Buildings.Add(new BuildingEntityProxy(buidingOrigin)));

            Buildings.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingEntity = e.Value;
                gameState.Buildings.Add(new BuildingEntity
                {
                    Id = addedBuildingEntity.Id,
                    TypeId = addedBuildingEntity.TypeId,
                    Level = addedBuildingEntity.Level.Value,
                    Position = addedBuildingEntity.Position.Value
                });
            });

            Buildings.ObserveRemove().Subscribe(e =>
            {
                var removedBuildingEntityProxy = e.Value;
                var removedBuildingEntity = gameState.Buildings.FirstOrDefault(b => b.Id == removedBuildingEntityProxy.Id);
                gameState.Buildings.Remove(removedBuildingEntity);
            });
        }
    }
}