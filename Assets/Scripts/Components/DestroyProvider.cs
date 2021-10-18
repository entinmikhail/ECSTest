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
    
    [Serializable]
    public struct TimerLogicComponent
    {
        public float timer;
        public float MaxTimerValue;
        
        public ILogicTagComponent LogicTagComponent;

    }

    public struct DieLogicTagComponent : ILogicTagComponent
    {
        
    }
    
    public interface ILogicTagComponent{}
    
    public class TimerLogicComponentProvider : MonoProvider<TimerLogicComponent> { }
}