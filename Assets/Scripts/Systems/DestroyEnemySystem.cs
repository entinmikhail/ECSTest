using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

public class DestroyEnemySystem : IEcsRunSystem
{
    private readonly EcsWorld _world = null;
    private EcsFilter<DestroyEnemyComponent, FoodBarComponent> _destroyFilter = null;

    private GameObject _predator = Resources.Load<GameObject>("Predator");
    private GameObject _bacterium = Resources.Load<GameObject>("Bacterium");
    
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
                        var go =  Object.Instantiate(_predator,
                            enemy._levelObject.transform.position, Quaternion.identity); 
                    }
                    else
                    {
                        var go =  Object.Instantiate(_bacterium,
                            enemy._levelObject.transform.position, Quaternion.identity); 
                    }
                }
            }
        }
    }
}