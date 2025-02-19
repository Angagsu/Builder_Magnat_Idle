using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using System.Linq;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdResourcesSpendhandler : ICommandHandler<CmdResourcesSpend>
    {
        private readonly GameStateProxy gameState;


        public CmdResourcesSpendhandler(GameStateProxy gameState)
        {
            this.gameState = gameState;
        }

        public bool Handle(CmdResourcesSpend command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = gameState.Resources.FirstOrDefault(resource =>
            resource.ResourceType == requiredResourceType);

            if (requiredResource == null)
            {
                Debug.LogError("Trying to spend not existed resource !!!");

                return false;
            }

            if (requiredResource.Amount.Value < command.Amount)
            {
                Debug.LogError($"Trying to spend more resources than existed ({requiredResourceType})." +
                    $" Exists: {requiredResource.Amount.Value}, trying to spend: {command.Amount}");

                return false;
            }

            requiredResource.Amount.Value -= command.Amount;

            return true;
        }
    }
}