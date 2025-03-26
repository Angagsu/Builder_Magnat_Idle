using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities.Mergeable.Buildings;
using Assets._BuilderMagnatIdle.Scripts.Game.State.Entities.Mergeable.ResourcesEntities;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Entities
{
    public static class EntitiesFactory 
    {
        public static Entity CreatEntity(EntityData entityData)
        {
            switch (entityData.Type)
            {
                case EntityType.Building:
                    return new BuildingEntity(entityData as BuildingEntityData);
                case EntityType.Resource:
                    return new ResourceEntity(entityData as ResourceEntityData);
                default:
                    throw new System.Exception("Unsupported entity type: " + entityData.Type);
            }
        }
        
    }
}