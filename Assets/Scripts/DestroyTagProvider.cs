using System;
using Voody.UniLeo;

namespace DefaultNamespace
{
    [Serializable] 
    public struct DestroyTag{}
    public class DestroyTagProvider : MonoProvider<DestroyTag> {}
}