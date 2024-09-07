using Assets._BuilderMagnatIdle.Scripts.Game.GameRoot;
using System.Collections;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; }


        public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}