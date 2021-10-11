using System;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct TransformComponent
    {
        public Transform ModelTransform;
    }
    public class ModelProvider : MonoProvider<TransformComponent> { }
}