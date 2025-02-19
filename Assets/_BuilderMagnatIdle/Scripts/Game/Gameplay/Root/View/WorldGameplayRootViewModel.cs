using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.Buildings;
using ObservableCollections;
using UnityEngine;
using R3;
using System;
using Random = UnityEngine.Random;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root.View
{
    public class WorldGameplayRootViewModel 
    {
        public readonly IObservableCollection<BuildingViewModel> AllBuildings;

        private readonly ResourcesService resourcesService;


        public WorldGameplayRootViewModel(BuildingsService buildingsService, ResourcesService resourcesService)
        {
            AllBuildings = buildingsService.AllBuildings;
            this.resourcesService = resourcesService;

            resourcesService.ObserveResource(ResourceType.SoftCurrency)
                .Subscribe(newValue => Debug.Log($"Soft Currency: {newValue}"));

            resourcesService.ObserveResource(ResourceType.HardCurrency)
                .Subscribe(newValue => Debug.Log($"Hard Currency: {newValue}"));

            resourcesService.ObserveResource(ResourceType.Wood)
                .Subscribe(newValue => Debug.Log($"Wood: {newValue}"));
        }

        public void HandleTestInput()
        {
            var rResourceType = (ResourceType)Random.Range(0, Enum.GetNames(typeof(ResourceType)).Length);
            var rValue = Random.Range(1, 1000);
            var rOperation = Random.Range(0, 2);

            if (rOperation == 0)
            {
                resourcesService.AddResource(rResourceType, rValue);
                return;
            }

            resourcesService.TrySpendResources(rResourceType, rValue);
        }
    }
}