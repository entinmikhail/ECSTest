using System;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct DestroyEnemyComponent
    {
        public Collider2D Target => _levelObject.Target;
        public bool IsEnemy => _levelObject.Target != null;
        [SerializeField] public LevelObject _levelObject;
    }
    public class DestroyEnemyProvider : MonoProvider<DestroyEnemyComponent> { }
}