using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Commands;
using Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.View.GameResources;
using Assets._BuilderMagnatIdle.Scripts.Game.State.cmd;
using Assets._BuilderMagnatIdle.Scripts.Game.State.GameResources;
using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;

namespace Assets._BuilderMagnatIdle.Scripts.Game.Gameplay.Services
{
    public class ResourcesService 
    {
        public readonly ObservableList<ResourceViewModel> Resources = new();

        private readonly Dictionary<ResourceType, ResourceViewModel> resourcesMap = new();
        private readonly ICommandProcessor cmd;


        public ResourcesService(ObservableList<Resource> resources, ICommandProcessor cmd)
        {
            this.cmd = cmd;
            resources.ForEach(CreateResourceViewModel);
            resources.ObserveAdd().Subscribe(observer => CreateResourceViewModel(observer.Value));
            resources.ObserveRemove().Subscribe(observer => RemoveResourceViewModel(observer.Value));
        }

        public bool AddResource(ResourceType resourceType, int amount)
        {
            var command = new CmdResourcesAdd(resourceType, amount);

            return cmd.Process(command);
        }

        public bool TrySpendResources(ResourceType resourceType, int amount)
        {
            var command = new CmdResourcesSpend(resourceType, amount);

            return cmd.Process(command);
        }

        public bool ISEnoughResources(ResourceType resourceType, int amount)
        {
            if (resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amount.CurrentValue >= amount;
            }

            return false;
        }

        public Observable<int> ObserveResource(ResourceType resourceType)
        {
            if (resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amount;
            }

            throw new Exception($"Resource of type {resourceType} doesn't exist");
        }

        private void CreateResourceViewModel(Resource resource)
        {
            var resourceViewModel = new ResourceViewModel(resource);
            resourcesMap[resource.ResourceType] = resourceViewModel;

            Resources.Add(resourceViewModel);
        }

        private void RemoveResourceViewModel(Resource resource)
        {
            if (resourcesMap.TryGetValue(resource.ResourceType, out var resourceViewModel))
            {
                Resources.Remove(resourceViewModel);
                resourcesMap.Remove(resource.ResourceType);
            }
        }
    }
}