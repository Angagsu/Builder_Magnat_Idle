using Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using R3;
using System;
using System.Collections;
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
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = CreatGameStateFromSettings();
                Debug.Log("Game State created from settings " + JsonUtility.ToJson(gameStateOrigin, true));

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                gameStateOrigin = JsonUtility.FromJson<GameState>(json);
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
                gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingsState>(json);
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
                Buildings = new List<BuildingEntity>
                {
                    new()
                    {
                        TypeId = "PRO100"
                    },
                    new()
                    {
                        TypeId = "STARIK"
                    }
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
            var json = JsonUtility.ToJson(gameSettingsStateOrigin, true);
            PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return Observable.Return(true);
        }
    }
}