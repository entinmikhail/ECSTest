using System;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable]
    public struct PlayerTag{}
    public class PlayerTagProvider : MonoProvider<PlayerTag> { }
    
}