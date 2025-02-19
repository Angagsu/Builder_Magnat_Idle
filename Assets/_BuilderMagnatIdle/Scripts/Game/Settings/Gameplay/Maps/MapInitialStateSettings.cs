using Assets._BuilderMagnatIdle.Scripts.Game.Settings.Gameplay.Buildings;
using System;
using System.Collections.Generic;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Settings.Gameplay.Maps
{
    [Serializable]
    public class MapInitialStateSettings 
    {
        public List<BuildingInitialStateSettings> Buildings;
    }
}