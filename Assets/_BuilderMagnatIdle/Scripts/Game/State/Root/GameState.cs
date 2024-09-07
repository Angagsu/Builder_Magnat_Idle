using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using System;
using System.Collections.Generic;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Root
{
    [Serializable]
    public class GameState 
    {
        public List<BuildingEntity> Buildings;
    }
}