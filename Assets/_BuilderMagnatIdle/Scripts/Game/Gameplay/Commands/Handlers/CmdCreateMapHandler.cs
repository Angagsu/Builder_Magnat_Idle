using Assets._BuilderMagnatIdle.Scripts.Game.Settings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities.Mergeable.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Maps;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdCreateMapHandler : ICommandHandler<CmdCreateMap>
    {
        private readonly GameStateProxy gameState;
        private readonly GameSettings gameSettings;


        public CmdCreateMapHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            this.gameState = gameState;
            this.gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateMap command)
        {
            var isMapAlreadyExist = gameState.Maps.Any(m => m.Id == command.MapId);

            if (isMapAlreadyExist)
            {
                Debug.LogError($"Map with Id = {command.MapId} already exists");
                return false;
            }

            var newMapSettings = gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);
            var newMapInitialStateSettings = newMapSettings.InitialStateSettings;

            var initialEntities = new List<EntityData>();
            
            foreach (var buildingSettings in newMapInitialStateSettings.Buildings)
            {
                var initialBuilding = new BuildingEntityData
                {
                    UniqueId = gameState.CreateEntityId(),
                    ConfigId = buildingSettings.TypeId,
                    Type = EntityType.Building,
                    Position = buildingSettings.Position,
                    Level = buildingSettings.Level,
                    IsAutoCollectionEnabled = false,
                    LastClickedTimeMS = 0
                };
            
                initialEntities.Add(initialBuilding);
            }

            var newMapState = new MapData
            {
                Id = command.MapId,
                Entities = initialEntities
            };

            var newMapStateProxy = new Map(newMapState);

            gameState.Maps.Add(newMapStateProxy);

            return true;
        }
    }
}