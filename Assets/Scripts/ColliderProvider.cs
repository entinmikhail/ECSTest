using System;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct ColliderComponent
    {
        public CircleCollider2D Collider;
        public float VisionRadius;
    }
    public class ColliderProvider : MonoProvider<ColliderComponent> { }
}