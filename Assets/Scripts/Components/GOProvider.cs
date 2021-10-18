using System;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct GOComponent
    {
        [SerializeField] public GameObject GameObject;
    }
    public class GOProvider : MonoProvider<GOComponent> { }
}