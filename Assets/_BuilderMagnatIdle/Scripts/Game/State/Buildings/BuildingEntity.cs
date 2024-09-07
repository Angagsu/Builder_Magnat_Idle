using System;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings
{
    [Serializable]
    public class BuildingEntity 
    {
        public int Id;
        public string TypeId;
        public Vector3Int Position;
        public int Level;
    }
}