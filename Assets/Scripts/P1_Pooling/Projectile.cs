using System.Threading;
using UnityEngine;

namespace P1_Pooling
{
    public class Projectile : MonoBehaviour
    {
        private float _totalTime;
        void Start()
        {
            FakeInitializeProjectile();
        }

        /// <summary>
        /// Setting up complex Prefabs containing Models, Sprites, Materials etc.
        /// Is Expensive. This Call simulates a more complex Object.
        /// Do not remove this Method invocation from Start.
        /// Instead, try to avoid `Start` being invoked in the first place. 
        /// </summary>
        void FakeInitializeProjectile()
        {
            Thread.Sleep(100);
        }
    
        // Update is called once per frame
        void Update()
        {
            this._totalTime += Time.deltaTime;
            this.transform.Translate(Vector3.up * Time.deltaTime);
            if (this._totalTime > 10f)
            {
                gameObject.SetActive(false);
                _totalTime = 0;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("On Collision!");
            gameObject.SetActive(false);
            _totalTime = 0;
        }
    }
}
