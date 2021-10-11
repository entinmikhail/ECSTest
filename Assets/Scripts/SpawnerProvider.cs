using System;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct SpawnComponent
    {
        public int MaxCuontOfFood;
        public int CurFoodValue;
        public GameObject go;
        public bool IsSpawned;
    }
    public class SpawnerProvider : MonoProvider<SpawnComponent>
    {
        
    }
}