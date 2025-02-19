using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot;
using System.Collections;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public int MapId { get; }


        public GameplayEnterParams(int mapId) : base(Scenes.GAMEPLAY)
        {
            MapId = mapId;
        }
    }
}