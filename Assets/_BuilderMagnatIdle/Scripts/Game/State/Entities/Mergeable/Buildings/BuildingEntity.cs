using R3;


namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Entities.Mergeable.Buildings
{
    public class BuildingEntity : MergeableEntity
    {
        public readonly ReactiveProperty<double> LastClickedTimeMS;
        public readonly ReactiveProperty<bool> IsAutoCollectionEnabled;


        public BuildingEntity(BuildingEntityData data) : base(data)
        {
            LastClickedTimeMS = new ReactiveProperty<double>(data.LastClickedTimeMS);
            LastClickedTimeMS.Subscribe(newLastClickedTime => data.LastClickedTimeMS = newLastClickedTime);

            IsAutoCollectionEnabled = new ReactiveProperty<bool>(data.IsAutoCollectionEnabled);
            IsAutoCollectionEnabled.Subscribe(newValue => data.IsAutoCollectionEnabled = newValue);
        }
    }
}