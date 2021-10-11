using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
using Voody.UniLeo;

public class EcsGameStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
        _systems.ConvertScene();

        AddInjections();
        AddSystems();
        AddOneFrame();
        
        _systems.Init();
    }

    private void AddSystems()
    {
        _systems
            .Add(new PlayerInputSystem())
            .Add(new PlayerMovementSystem())
            .Add(new MovementSystem())
            .Add(new FoodSpawnSystem())
            .Add(new TimerSystem())
            .Add(new ObjectDestroyerSystem())
            .Add(new DestroyEnemySystem());

    }

    private void AddInjections()
    {
        
    }

    private void AddOneFrame()
    {
        
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        if (_systems == null) return;
        
        _systems.Destroy();
        _systems = null;
        _world.Destroy();
        _world = null;
    }
}
