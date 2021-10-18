using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

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
                for (;spawner.CurFoodValue < spawner.MaxCuontOfFood; spawner.CurFoodValue++)
                {
                    Object.Instantiate(spawner.go, 
                        new Vector3(Random.Range(-70, 60), Random.Range(-44, 44), 0), Quaternion.identity);
                }
                spawner.IsSpawned = true;
            }
        }
    }
}