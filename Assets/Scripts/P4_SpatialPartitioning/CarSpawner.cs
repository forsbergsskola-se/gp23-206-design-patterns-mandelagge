using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace P4_SpatialPartitioning
{
    public class CarSpawner : MonoBehaviour
    {
        public Car CarPrefab;
        private float _currentCooldown;
    
        const float _totalCooldown = 1f;

        private QuadTree _quadTree;

        private void Awake()
        {
            _quadTree = new QuadTree(0, new Bounds(Vector3.zero, new Vector3(120, 120, 0)));
        }

        public QuadTree GetQuadTree()
        {
            return _quadTree;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            this._currentCooldown -= Time.deltaTime;
            if (this._currentCooldown <= 0f)
            {
                this._currentCooldown += _totalCooldown;
                SpawnCar();
            }
        }

        void SpawnCar()
        {
            var randomPositionX = Random.Range(-60f, 60f);
            var randomPositionY = Random.Range(-60f, 60f);
            Car car = Instantiate(this.CarPrefab, new Vector2(randomPositionX, randomPositionY), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            _quadTree.Insert(car.gameObject);
        }
        
        
        private void OnDrawGizmos()
        {
            if (_quadTree != null)
            {
                DrawNodeBounds(_quadTree);
            }
        }

        private void DrawNodeBounds(QuadTree node)
        {
            // Draw the bounds of the current node
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(node.bounds.center, node.bounds.size);

            // Recursively draw bounds for child nodes
            if (node.nodes != null)
            {
                foreach (QuadTree childNode in node.nodes)
                {
                    if (childNode != null)
                    {
                        DrawNodeBounds(childNode);
                    }
                }
            }
        }
    }
}
