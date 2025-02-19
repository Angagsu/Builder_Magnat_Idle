using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.Settings.Gameplay.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services
{
    public class BuildingsService 
    {
        private readonly ICommandProcessor cmd;
        private readonly ObservableList<BuildingViewModel> allBuildings = new();
        private readonly Dictionary<int, BuildingViewModel> buildingsMap = new();
        private readonly Dictionary<string, BuildingSettings> buildingSettingsMap = new();

        public IObservableCollection<BuildingViewModel> AllBuildings => allBuildings;

        public BuildingsService(
            IObservableCollection<BuildingEntityProxy> buildings,
            BuildingsSettings buildingsSettings,
            ICommandProcessor cmd)
        {
            this.cmd = cmd;

            foreach (var buildingSettings in buildingsSettings.AllBuildings)
            {
                buildingSettingsMap[buildingSettings.TypeId] = buildingSettings;
            }

            foreach (var buildingEntity in buildings)
            {
                CreateBuildingViewModel(buildingEntity);
            }

            buildings.ObserveAdd().Subscribe(e =>
            {
                CreateBuildingViewModel(e.Value);
            });

            buildings.ObserveRemove().Subscribe(e =>
            {
                RemoveBuildingViewModel(e.Value);
            });
        }

        public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
        {
            var command = new CmdPlaceBuliding(buildingTypeId, position);
            var result = cmd.Process(command);

            return result;
        }

        public bool MoveBuilding(int buildingEntityId, Vector3Int newPosition)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBuilding(int buildingEntityId)
        {
            throw new NotImplementedException();
        }

        private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
        {
            var buildingSettings = buildingSettingsMap[buildingEntity.TypeId];
            var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);

            allBuildings.Add(buildingViewModel);
            buildingsMap[buildingEntity.Id] = buildingViewModel;
        }

        private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
        {
            if (buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
            {
                allBuildings.Remove(buildingViewModel);
                buildingsMap.Remove(buildingEntity.Id);
            }
        }
    }
}