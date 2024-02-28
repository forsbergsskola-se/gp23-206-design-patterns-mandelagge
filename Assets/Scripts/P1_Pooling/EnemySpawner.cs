using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace P1_Pooling
{
    public class EnemySpawner : MonoBehaviour
    {
        public Enemy EnemyPrefab;
        private const float _totalCooldown = 2f;
        private float _currentCooldown;

        public List<Enemy> enemyPool = new List<Enemy>();
        public int maxEnemies;

        [FormerlySerializedAs("enemiesToAdd")] public int enemiesToCreate;
    
        private void Awake()
        {
            for(int i = 0; i < maxEnemies; i++)
            {
                CreateNewEnemy();
            }
        }

        Enemy CreateNewEnemy()
        {
            Enemy enemy = Instantiate(this.EnemyPrefab, new Vector2(0, 0), Quaternion.identity);
            enemyPool.Add(enemy);
            enemy.gameObject.SetActive(false);
        
            return enemy;
        }


        // Update is called once per frame
        void Update()
        {
            this._currentCooldown -= Time.deltaTime;
            if (this._currentCooldown <= 0f)
            {
                this._currentCooldown += _totalCooldown;
                SpawnEnemies();
            }
            if (enemiesToCreate > 0)
            {
                CreateNewEnemy();
                maxEnemies++;
                enemiesToCreate--;
            }
        
        
        }

    

        void SpawnEnemies()
        {
            var maxAmount = Mathf.CeilToInt(Time.timeSinceLevelLoad / 7);
            int amount = Random.Range(maxAmount, maxAmount + 3);
            for (var i = 0; i < amount; i++)
            {
                SpawnEnemy();
            }

            int enemyActiveCount = enemyPool.Count(enemy => enemy.gameObject.activeSelf);

            if (enemyActiveCount >= maxEnemies / 1.5f)
            {
                enemiesToCreate = (int)(enemyActiveCount - maxEnemies / 1.5f);
            }
        }

        void SpawnEnemy()
        {
            Enemy enemy = enemyPool.FirstOrDefault(en => en.gameObject.activeSelf == false);

            if (enemy == null)
            {
                Debug.Log("NO MORE ENEMIES IN LIST");
                return;
            }
        
            var randomPositionX = Random.Range(-6f, 6f);
            var randomPositionY = Random.Range(-6f, 6f);

            enemy.gameObject.SetActive(true);
            enemy.transform.position = new Vector2(randomPositionX, randomPositionY);
        }
    }
}
