//using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
//using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
//using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities;
//using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
//using System.Linq;
//using UnityEngine;


//namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands.Handlers
//{
//    public class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuliding>
//    {
//        private readonly GameStateProxy gameState;

//        public CmdPlaceBuildingHandler(GameStateProxy gameState)
//        {
//            this.gameState = gameState;
//        }

//        public bool Handle(CmdPlaceBuliding command)
//        {
//            var currentMap = gameState.Maps.FirstOrDefault(m => m.Id == gameState.CurrentMapId.CurrentValue);
//            if (currentMap == null)
//            {
//                Debug.LogError($"Couldn't find MapState for id: {gameState.CurrentMapId.CurrentValue}");
//            }

//            var entityId = gameState.CreateEntityId();
//            var newBuildingEntity = new BuildingEntity 
//            {
//                Id =  entityId,
//                Position = command.Position,
//                TypeId = command.BuildingTypeId
//            };

//            var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);
//            currentMap.Buildings.Add(newBuildingEntityProxy);

//            return true;
//        }
//    }
//}