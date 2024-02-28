using Game.Runtime.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Runtime.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsFilterInject<Inc<UnitCmp>> _unitCmpFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _unitCmpFilter.Value)
            {
                var unitCmp = _unitCmpPool.Value.Get(entity);
                var velocity = unitCmp.Velocity;
                var view = unitCmp.View;
                
                view.UpdateAnimationState(velocity);

                if (velocity == Vector3.zero)
                    continue;

                var translation = velocity * Time.deltaTime;
                view.SetDirection(velocity);
                view.Move(translation);
            }
        }
    }
}