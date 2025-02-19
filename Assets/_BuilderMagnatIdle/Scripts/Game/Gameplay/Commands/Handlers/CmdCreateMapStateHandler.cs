using Assets._BuilderMagnatIdle.Scripts.Game.Settings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Maps;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdCreateMapStateHandler : ICommandHandler<CmdCreateMapState>
    {
        private readonly GameStateProxy gameState;
        private readonly GameSettings gameSettings;


        public CmdCreateMapStateHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            this.gameState = gameState;
            this.gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateMapState command)
        {
            var isMapAlreadyExist = gameState.Maps.Any(m => m.Id == command.MapId);

            if (isMapAlreadyExist)
            {
                Debug.LogError($"Map with Id = {command.MapId} already exists");
                return false;
            }

            var newMapSettings = gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);
            var newMapInitialStateSettings = newMapSettings.InitialStateSettings;
            var initialBuildings = new List<BuildingEntity>();

            foreach (var buildingSettings in newMapInitialStateSettings.Buildings)
            {
                var initialBuilding = new BuildingEntity
                {
                    Id = gameState.CreateEntityId(),
                    TypeId = buildingSettings.TypeId,
                    Position = buildingSettings.Position,
                    Level = buildingSettings.Level
                };

                initialBuildings.Add(initialBuilding);
            }

            var newMapState = new MapState
            {
                Id = command.MapId,
                Buildings = initialBuildings
            };

            var newMapStateProxy = new Map(newMapState);

            gameState.Maps.Add(newMapStateProxy);

            return true;
        }
    }
}