using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsFilter<PlayerTag, DirectionComponent> _directionFilter = null;

    private float _moveX;
    private float _moveY;
    
    public void Run()
    {
        
        SetDirection();
        foreach (var i in _directionFilter)
        {
            ref var directionComponent = ref _directionFilter.Get2(i);
            ref var direction = ref directionComponent.Direction;
            direction.x = _moveX;
            direction.y = _moveY;  
        }
    }

    private void SetDirection()
    {
        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxis("Vertical");
    }
}