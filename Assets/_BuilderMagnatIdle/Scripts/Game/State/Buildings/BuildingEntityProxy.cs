﻿using R3;
using UnityEngine;

namespace Assets._BuilderMagnatIdle.Scripts.Game.State.Buildings
{
    public class BuildingEntityProxy 
    {
        public int Id { get; }
        public string TypeId { get; }

        public ReactiveProperty<Vector3Int> Position { get; }
        public ReactiveProperty<int> Level { get; }

        public BuildingEntityProxy(BuildingEntity buildingEntity)
        {
            Id = buildingEntity.Id;
            TypeId = buildingEntity.TypeId;

            Position = new ReactiveProperty<Vector3Int>(buildingEntity.Position);
            Level = new ReactiveProperty<int>(buildingEntity.Level);

            Position.Skip(1).Subscribe(value => buildingEntity.Position = value);
            Level.Skip(1).Subscribe(value => buildingEntity.Level = value);
        }
    }
}