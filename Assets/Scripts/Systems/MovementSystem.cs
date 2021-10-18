using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;


public class MovementSystem : IEcsRunSystem
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
            
            ref var transform = ref modelComponent.ModelTransform;
            ref var speed = ref movableComponent.Speed;

            if (directionComponent.Coliders.Count != 0)
            {
                if (directionComponent.Coliders.Peek() == null
                    || transform.position == directionComponent.Coliders.Peek().transform.position)
                {
                    directionComponent.Coliders.Dequeue();
                    
                }
                else
                {
                    var velocity = Vector2.MoveTowards(transform.position,
                        directionComponent.Coliders.Peek().transform.position, 
                        speed / 20 * Time.deltaTime);
                    transform.position = velocity;
                    directionComponent.IsRandomDirectionSet = false;
                }
            }
            else
            {
                if (!directionComponent.IsRandomDirectionSet)
                {
                    directionComponent.RandomDirection =
                        new Vector3(Random.Range(Random.Range(-3, -1), Random.Range(1, 3)), 
                            Random.Range(Random.Range(-3, -1), Random.Range(1, 3)), 0)
                        * Time.deltaTime * speed / 120 ;
                    directionComponent.IsRandomDirectionSet = true;
                }
                
                transform.position -= directionComponent.RandomDirection;
            }
        }
    }
}


