using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelObject : MonoBehaviour
    {
        public string Tag;
        public Collider2D Target;
        public bool IsFood = false;
        public Queue<Collider2D> Colliders = new Queue<Collider2D>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tag))
            {
                IsFood = true;
                Target = other;
                Colliders.Enqueue(Target);
            }
            else
            {
                IsFood = false;
            }
        }
    }
}