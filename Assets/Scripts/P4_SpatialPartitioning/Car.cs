using UnityEngine;
using Random = UnityEngine.Random;

namespace P4_SpatialPartitioning
{
    public class Car : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
        }
    }
}
