using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot.Services;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.MainMenu.Services
{
    public class SomeMainMenuService 
    {
        public readonly SomeCommonService someCommonService;

        public SomeMainMenuService(SomeCommonService someCommonService)
        {
            this.someCommonService = someCommonService;
            Debug.Log(GetType().Name + " has been created");
        }
    }
}