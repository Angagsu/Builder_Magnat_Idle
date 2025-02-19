using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using Assets._BuilderMagnatIdle.Scripts.Game.State.GameResources;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Root;
using System.Linq;


namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdResourcesAddHandler : ICommandHandler<CmdResourcesAdd>
    {
        private readonly GameStateProxy gameState;


        public CmdResourcesAddHandler(GameStateProxy gameState)
        {
            this.gameState = gameState;
        }


        public bool Handle(CmdResourcesAdd command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = gameState.Resources.FirstOrDefault(resource =>
            resource.ResourceType == requiredResourceType);

            if (requiredResource == null)
            {
                requiredResource = CreatNewResource(requiredResourceType);
            }

            requiredResource.Amount.Value += command.Amount;

            return true;
        }

        private Resource CreatNewResource(ResourceType resourceType)
        {
            var newResourceData = new ResourceData
            {
                ResourceType = resourceType,
                Amount = 0
            };

            var newResource = new Resource(newResourceData);
            gameState.Resources.Add(newResource);

            return newResource;
        }
    }
}