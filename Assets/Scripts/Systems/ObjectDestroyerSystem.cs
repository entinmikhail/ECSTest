using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

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