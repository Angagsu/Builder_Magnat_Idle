using Assets._BuilderMagnatIdle.Scripts.Game.State.GameResources;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Maps;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using Newtonsoft.Json;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

        public GameStateProxy GameState { get; private set; }
        public GameSettingsStateProxy SettingsState { get; private set; }

        private GameState gameStateOrigin;
        private GameSettingsState gameSettingsStateOrigin;



        public Observable<GameStateProxy> LoadGameState()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };

            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = CreatGameStateFromSettings();
                Debug.Log("Game State created from settings " + JsonConvert.SerializeObject(gameStateOrigin, Formatting.Indented));

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                gameStateOrigin = JsonConvert.DeserializeObject<GameState>(json);
                GameState = new GameStateProxy(gameStateOrigin);
                Debug.Log("Game State Loaded: " + json);
            }

            return Observable.Return(GameState);
        }

        public Observable<GameSettingsStateProxy> LoadSettingsState()
        {
            if (!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY))
            {
                SettingsState = CreatGameSettingsStateFromSettings();

                SaveSettingsState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_SETTINGS_STATE_KEY);
                gameSettingsStateOrigin = JsonConvert.DeserializeObject<GameSettingsState>(json);
                SettingsState = new GameSettingsStateProxy(gameSettingsStateOrigin);
            }

            return Observable.Return(SettingsState);
        }


        private GameSettingsStateProxy CreatGameSettingsStateFromSettings()
        {
            gameSettingsStateOrigin = new GameSettingsState()
            {
                MusicVolume = 8,
                SFXVolume = 8
            };

            return new GameSettingsStateProxy(gameSettingsStateOrigin);
        }

        private GameStateProxy CreatGameStateFromSettings()
        {
            gameStateOrigin = new GameState
            {
                Maps = new List<MapData>(),
                Resources = new List<ResourceData>()
                {
                    new() {Amount = 0, ResourceType = ResourceType.SoftCurrency},
                    new() {Amount = 0, ResourceType = ResourceType.HardCurrency},
                    new() {Amount = 0, ResourceType = ResourceType.Wood}
                }
            };

            return new GameStateProxy(gameStateOrigin);
        }


        public Observable<bool> ResetGameState()
        {
            GameState = CreatGameStateFromSettings();
            SaveGameState();

            return Observable.Return(true);
        }

        public Observable<GameSettingsStateProxy> ResetSettingsState()
        {
            SettingsState = CreatGameSettingsStateFromSettings();
            SaveSettingsState();

            return Observable.Return(SettingsState);
        }


        public Observable<bool> SaveSettingsState()
        {
            var json = JsonConvert.SerializeObject(gameSettingsStateOrigin, Formatting.Indented);
            PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonConvert.SerializeObject(gameStateOrigin, Formatting.Indented);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return Observable.Return(true);
        }
    }
}