using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

public class DestroyEnemySystem : IEcsRunSystem
{
    private readonly EcsWorld _world = null;
    private EcsFilter<DestroyEnemyComponent, FoodBarComponent> _destroyFilter = null;

    public void Run()
    {
        foreach (var i in _destroyFilter)
        {
            ref var enemy = ref _destroyFilter.Get1(i);
            ref var foodBar = ref _destroyFilter.Get2(i);
            
            if (enemy.IsEnemy)
            {
                Object.Destroy(enemy.Target.gameObject);
                foodBar.CountOfFood += 10;
                
                ref var timer = ref _destroyFilter.GetEntity(i).Get<DestroyComponent>();
                timer.timer = timer.MaxTimerValue;
                
                if (foodBar.CountOfFood >= foodBar.MaxCountOfFood)
                {
                    foodBar.CountOfFood -= foodBar.MaxCountOfFood;

                    if (Random.Range(1, 100) > 98 || foodBar.ID == "Predator")
                    {
                        var go =  Object.Instantiate(Resources.Load<GameObject>("Predator"),
                            enemy._levelObject.transform.position, Quaternion.identity); 
                    }
                    else
                    {
                        var go =  Object.Instantiate(Resources.Load<GameObject>("Bacterium"),
                            enemy._levelObject.transform.position, Quaternion.identity); 
                    }
                }
            }
        }
    }
}

public class TimerSystem : IEcsRunSystem
{
    private EcsFilter<DestroyComponent> _movebleFilter = null;
    public void Run()
    {
        foreach (var i in _movebleFilter)
        {
            if (_movebleFilter.GetEntity(i).Get<GOComponent>().GameObject == null) continue;

            ref var destroyComponent = ref _movebleFilter.Get1(i);
            destroyComponent.timer -= Time.deltaTime;
            if (destroyComponent.timer <= 0)
            {
                ref var entity = ref _movebleFilter.GetEntity(i);
                var go = entity.Get<TransformComponent>().ModelTransform.gameObject;
                if (entity.Get<FoodBarComponent>().ID == "Predator")
                {
                    for (int j = 0; j < 6; j++)
                    {
                        var goFood = Object.Instantiate(Resources.Load<GameObject>("Food"),
                            go.transform.position + new Vector3(Random.value, Random.value, 0), Quaternion.identity);
                        goFood.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
                else
                {
                    var goFood = Object.Instantiate(Resources.Load<GameObject>("Food"),
                        go.transform.position, Quaternion.identity);
                    goFood.GetComponent<SpriteRenderer>().color = Color.red;
                }

                entity.Get<DestroyTag>();
            }
        }
    }
}
public class ObjectDestroyerSystem : IEcsRunSystem
{
    private EcsFilter<DestroyTag> _destroyFilter = null;
    
    public void Run()
    {
        foreach (var i in _destroyFilter)
        {
          ref var entity = ref _destroyFilter.GetEntity(i);
          
          Object.Destroy(entity.Get<GOComponent>().GameObject);
          entity.Destroy();
        }
    }
}

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

public class FoodSpawnSystem : IEcsRunSystem
{
    private EcsFilter<SpawnComponent> _spawnFilter = null;
    public void Run()
    {
        foreach (var i  in _spawnFilter)
        {
            ref var spawner = ref _spawnFilter.Get1(i);
            if (!spawner.IsSpawned)
            {
                for ( ;spawner.CurFoodValue < spawner.MaxCuontOfFood; spawner.CurFoodValue++)
                {
                    Object.Instantiate(spawner.go, 
                        new Vector3(Random.Range(-70, 60), Random.Range(-44, 44), 0), Quaternion.identity);
                }

                spawner.IsSpawned = true;
            }
            
        }
    }
}


public class MovementSystem : IEcsRunSystem
{
    private readonly EcsWorld _world = null;
    private EcsFilter<TransformComponent, DirectionComponent, MovableComponent> _movebleFilter = null;
    
        
    public void Run()
    {
        foreach (var i in _movebleFilter)
        {
            if (_movebleFilter.Get1(i).ModelTransform == null) continue;

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


