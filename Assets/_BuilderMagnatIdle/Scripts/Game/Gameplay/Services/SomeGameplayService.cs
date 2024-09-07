using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using ObservableCollections;
using System;
using UnityEngine;
using R3;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using System.Linq;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services
{
    public class SomeGameplayService : IDisposable
    {
        public readonly SomeCommonService someCommonService;

        private GameStateProxy gameState;

        public SomeGameplayService(GameStateProxy gameState, SomeCommonService someCommonService)
        {
            this.gameState = gameState;
            this.someCommonService = someCommonService;
            Debug.Log(GetType().Name + " has been created");

            gameState.Buildings.ForEach(b => Debug.Log($"Building: {b.TypeId}"));
            gameState.Buildings.ObserveAdd().Subscribe(e => Debug.Log($"Building added: {e.Value.TypeId}"));
            gameState.Buildings.ObserveRemove().Subscribe(e => Debug.Log($"Building removed: {e.Value.TypeId}"));

            AddBuilding("Vasyan");
            AddBuilding("Stasyan");
            AddBuilding("Reklama");

            RemoveBuilding("Reklama");
        }

        public void Dispose()
        {
            Debug.Log("Clear all subscribers");
        }

        private void AddBuilding(string buildingTypeId)
        {
            var building = new BuildingEntity
            {
                TypeId = buildingTypeId
            };
            var buildingProxy = new BuildingEntityProxy(building);
            gameState.Buildings.Add(buildingProxy);
        }

        private void RemoveBuilding(string buildingTypeId)
        {
            var buildingEntity = gameState.Buildings.FirstOrDefault(b => b.TypeId == buildingTypeId);

            if (buildingEntity != null)
            {
                gameState.Buildings.Remove(buildingEntity);
            }
        }
    }
}