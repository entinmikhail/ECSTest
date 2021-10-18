using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerMovementSystem : IEcsRunSystem
{
    private readonly EcsWorld _world = null;
    private EcsFilter<TransformComponent, DirectionComponent, MovableComponent> _movebleFilter = null;
    
        
    public void Run()
    {
        foreach (var i in _movebleFilter)
        {
            if (_movebleFilter.Get1(i).ModelTransform == null)
            {
                _movebleFilter.GetEntity(i).Destroy();
                continue;
            }

            ref var modelComponent = ref _movebleFilter.Get1(i);
            ref var directionComponent = ref _movebleFilter.Get2(i);
            ref var movableComponent = ref _movebleFilter.Get3(i);

            ref var direction = ref directionComponent.Direction;
            ref var transform = ref modelComponent.ModelTransform;
            ref var rigidbody = ref movableComponent.Rigidbody;
            ref var speed = ref movableComponent.Speed;
            
            var rawDirection =
                (transform.right * direction.x + transform.up * direction.y) * speed * Time.deltaTime;

            rigidbody.AddForce(rawDirection);
        }
    }
}