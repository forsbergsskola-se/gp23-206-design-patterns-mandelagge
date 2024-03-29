using UnityEngine;
using Random = UnityEngine.Random;

namespace P2_Flyweight
{
    public class TreeSpawner : MonoBehaviour
    {
        public Tree TreePrefab;
        private float _currentCooldown;
    
        const float _totalCooldown = 0.2f;

        private TreeSeasonColors _treeColors;
        
        private void Awake()
        {
            var fileContents = Resources.Load<TextAsset>("treeColors").text;
            this._treeColors = JsonUtility.FromJson<TreeSeasonColors>(fileContents);
        }


        void Update()
        {
            this._currentCooldown -= Time.deltaTime;
            if (this._currentCooldown <= 0f)
            {
                this._currentCooldown += _totalCooldown;
                SpawnTree();
            }
        }

        void SpawnTree()
        {
            var randomPositionX = Random.Range(-6f, 6f);
            var randomPositionY = Random.Range(-6f, 6f);
            Instantiate(this.TreePrefab, new Vector2(randomPositionX, randomPositionY), Quaternion.identity);
        }

        public TreeSeasonColors GetSeasonColors()
        {
            return _treeColors;
        }

        
    }
}
