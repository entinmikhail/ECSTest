using System;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct DirectionComponent
    {
        public Vector2 Direction;
        public Vector3 RandomDirection;
        public bool IsRandomDirectionSet;
        public bool IsFood => _levelObject.IsFood ;
        public Collider2D Target => _levelObject.Target;
        public Queue<Collider2D> Coliders => _levelObject.Colliders;
        [SerializeField] public LevelObject _levelObject;
    }
    public class DirectionProvider : MonoProvider<DirectionComponent> { }
  
}