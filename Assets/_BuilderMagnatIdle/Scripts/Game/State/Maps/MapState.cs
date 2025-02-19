using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities;
using System;
using System.Collections.Generic;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Maps
{
    [Serializable]
    public class MapState
    {
        public int Id;
        public List<BuildingEntity> Buildings;
    }
}