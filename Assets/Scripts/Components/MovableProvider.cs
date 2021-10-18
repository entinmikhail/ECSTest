using System;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct MovableComponent
    {
        public float Speed;
        public Rigidbody2D Rigidbody;
    }

    public class MovableProvider : MonoProvider<MovableComponent> { }
}