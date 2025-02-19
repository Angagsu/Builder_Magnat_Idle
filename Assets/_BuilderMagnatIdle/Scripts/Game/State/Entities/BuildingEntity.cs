using System;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Entities
{
    [Serializable]
    public class BuildingEntity : Entity
    {
        public string TypeId;
        public Vector3Int Position;
        public int Level;
    }
}