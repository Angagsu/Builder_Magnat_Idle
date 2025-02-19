using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands
{
    public class CmdPlaceBuliding : ICommand
    {
        public readonly string BuildingTypeId;
        public readonly Vector3Int Position;

        public CmdPlaceBuliding(string buildingTypeId, Vector3Int position)
        {
            BuildingTypeId = buildingTypeId;
            Position = position;
        }
    }
}