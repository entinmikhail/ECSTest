using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelObject : MonoBehaviour
    {
        public string Tag;
        public Collider2D Target;
        public Queue<Collider2D> Colliders = new Queue<Collider2D>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tag))
            {
                Target = other;
                Colliders.Enqueue(Target);
            }
        }
    }
}