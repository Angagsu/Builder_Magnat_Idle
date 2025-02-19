using System.Threading.Tasks;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Settings
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GameSettings => gameSettings;

        public ApplicationSettings ApplicationSettings { get; }

        private GameSettings gameSettings;



        public SettingsProvider()
        {
            ApplicationSettings = Resources.Load<ApplicationSettings>(path:"ApplicationSettings");
        }

        public Task<GameSettings> LoadGameSettings()
        {
            gameSettings = Resources.Load<GameSettings>(path: "GameSettings");

            return Task.FromResult(gameSettings);
        }
    }
}