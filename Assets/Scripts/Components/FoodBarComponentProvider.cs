using System;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct FoodBarComponent
    {
        public int CountOfFood;
        public int MaxCountOfFood;
        public string ID;
    }
    public class FoodBarComponentProvider : MonoProvider<FoodBarComponent> { }
}