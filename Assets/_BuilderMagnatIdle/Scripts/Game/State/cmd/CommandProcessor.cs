using System;
using System.Collections;
using System.Collections.Generic;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.cmd
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly Dictionary<Type, object> handlesMap = new Dictionary<Type, object>();
        private IGameStateProvider gameStateProvider;

        public CommandProcessor(IGameStateProvider gameStateProvider)
        {
            this.gameStateProvider = gameStateProvider;
        }

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            handlesMap[typeof(TCommand)] = handler;
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (handlesMap.TryGetValue(typeof(TCommand), out var handler))
            {
                var typedHandler = (ICommandHandler<TCommand>)handler;
                var result = typedHandler.Handle(command);

                if (result)
                {
                    gameStateProvider.SaveGameState();
                }

                return result;
            }

            return false;
        }        
    }
}