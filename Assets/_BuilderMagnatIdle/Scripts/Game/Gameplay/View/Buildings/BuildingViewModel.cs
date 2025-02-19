using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services;
using Assets._BuilderMagnatIdle.Scripts.Game.Settings.Gameplay.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using R3;
using System.Collections.Generic;
using UnityEngine;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.Buildings
{
    public class BuildingViewModel 
    {
        private readonly BuildingEntityProxy buildingEntity;
        private readonly BuildingsService buildingsService;
        private readonly BuildingSettings buildingSettings;
        private readonly Dictionary<int, BuildingLevelSettings> levelSettingsMap = new();

        public readonly int BuildingEntityId;
        public readonly string TypId;
        public ReadOnlyReactiveProperty<Vector3Int> Position { get; }
        public ReadOnlyReactiveProperty<int> Level { get; }

        public BuildingViewModel(
            BuildingEntityProxy buildingEntity,
            BuildingSettings buildingSettings,
            BuildingsService buildingsService)
        {
            this.buildingEntity = buildingEntity;
            this.buildingsService = buildingsService;
            this.buildingSettings = buildingSettings;

            Level = buildingEntity.Level;
            BuildingEntityId = buildingEntity.Id;
            TypId = buildingSettings.TypeId;

            foreach (var buildingLevelSettings in buildingSettings.LevelsSettings)
            {
                levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
            }

            Position = buildingEntity.Position;
        }

        public BuildingLevelSettings GetLevelSettings(int level)
        {
            return levelSettingsMap[level];
        }
    }
}