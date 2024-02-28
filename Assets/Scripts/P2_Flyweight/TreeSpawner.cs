using UnityEngine;
using Random = UnityEngine.Random;

namespace P2_Flyweight
{
    public class TreeSpawner : MonoBehaviour
    {
        public Tree TreePrefab;
        private float _currentCooldown;
    
        const float _totalCooldown = 0.2f;

        private string fileContents;
        private TreeData _treeData;
    
        private void Awake()
        {
            // fileContents = Resources.Load<TextAsset>("treeColors").text;
            _treeData = new TreeData();
            _treeData.LoadColorInfos();
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

        public string GetColorInfo()
        {
            return fileContents;
        }

        public TreeData GetTreeData()
        {
            return _treeData;
        }
    }
}
