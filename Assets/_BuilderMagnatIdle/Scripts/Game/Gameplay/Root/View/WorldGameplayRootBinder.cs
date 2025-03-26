using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.Buildings;
using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root.View
{
    public class WorldGameplayRootBinder : MonoBehaviour
    {
        private readonly Dictionary<int, BuildingBinder> createdBuildingsMap = new();
        private readonly CompositeDisposable disposables = new();

        private WorldGameplayRootViewModel viewModel;


        public void Bind(WorldGameplayRootViewModel viewModel)
        {
            this.viewModel = viewModel;

           //foreach (var buildingViewModel in viewModel.AllBuildings)
           //{
           //    CreateBuilding(buildingViewModel);
           //}
           //
           //disposables.Add(viewModel.AllBuildings.ObserveAdd()
           //    .Subscribe(e => CreateBuilding(e.Value)));
           //
           //disposables.Add(viewModel.AllBuildings.ObserveRemove().Subscribe(e => DestroyBuilding(e.Value)));
        }

        private void OnDestroy()
        {
            disposables.Dispose();
        }

        private void CreateBuilding(BuildingViewModel buildingViewModel)
        {
            var buildingLevel = buildingViewModel.Level.CurrentValue;
            var buildingType = buildingViewModel.TypId;
            var prefabBuildingLevelPath = $"Prefabs/Gameplay/Buildings/Building_{buildingType}_{buildingLevel}";
            var buildingPrefab = Resources.Load<BuildingBinder>(prefabBuildingLevelPath);

            var createdBuilding = Instantiate(buildingPrefab);
            createdBuilding.Bind(buildingViewModel);

            createdBuildingsMap[buildingViewModel.BuildingEntityId] = createdBuilding;
        }

        private void DestroyBuilding(BuildingViewModel buildingViewModel) 
        {
            if (createdBuildingsMap.TryGetValue(buildingViewModel.BuildingEntityId, out var buildingBinder))
            {
                Destroy(buildingBinder.gameObject);
                createdBuildingsMap.Remove(buildingViewModel.BuildingEntityId);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                viewModel.HandleTestInput();
            }
        }
    }
}