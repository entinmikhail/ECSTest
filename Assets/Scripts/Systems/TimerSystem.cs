using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

public class TimerSystem : IEcsRunSystem
{
    private EcsFilter<DestroyComponent> _movebleFilter = null;
    private EcsFilter<TimerLogicComponent> _logicFilter = null;

    private GameObject _foodGo = Resources.Load<GameObject>("Food");
    public void Run()
    {
        foreach (var i in _movebleFilter)
        {
            if (_movebleFilter.GetEntity(i).Get<GOComponent>().GameObject == null) continue;

            ref var destroyComponent = ref _movebleFilter.Get1(i);
            destroyComponent.timer -= Time.deltaTime;
            if (destroyComponent.timer <= 0)
            {
                ProcessTimerEnd(i);
            }
        }
    }

    private void ProcessTimerEnd(int i)
    {
        ref var entity = ref _movebleFilter.GetEntity(i);
        var go = entity.Get<TransformComponent>().ModelTransform.gameObject;
        if (entity.Get<FoodBarComponent>().ID == "Predator")
        {
            for (int j = 0; j < 6; j++)
            {
                var goFood = Object.Instantiate(_foodGo,
                    go.transform.position + new Vector3(Random.value, Random.value, 0),
                    Quaternion.identity);
                goFood.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            var goFood = Object.Instantiate(_foodGo, go.transform.position, Quaternion.identity);
            goFood.GetComponent<SpriteRenderer>().color = Color.red;
        }

        entity.Get<DestroyTag>();
    } 
    private void ProcessTimerEndNew(int i)
    {
        ref var logic  = ref _logicFilter.Get1(i);
        ref var entity = ref _logicFilter.GetEntity(i);
        var a = logic.LogicTagComponent.GetType();
        var type = new DieLogicTagComponent().GetType();

    }
}