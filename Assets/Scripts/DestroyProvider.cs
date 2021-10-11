using System;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct DestroyComponent
    {
        public float timer;
        public float MaxTimerValue;
    }
    public class DestroyProvider : MonoProvider<DestroyComponent> { }
}