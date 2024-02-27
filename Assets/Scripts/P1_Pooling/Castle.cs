using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public Projectile Projectile;

    private Transform _target;
    private int _enemyLayerMask;
    private float _currentCooldown;
    
    const float _maxCooldown = 0.8f;

    private List<Projectile> projectilePool = new List<Projectile>();
    public int maxProjectiles;
    
    void Start()
    {
        this._enemyLayerMask = LayerMask.GetMask("Enemy");

        for (int i = 0; i < maxProjectiles; i++)
            CreateNewProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        AcquireTargetIfNecessary();
        TryAttack();
    }
    
    Projectile CreateNewProjectile()
    {
        Projectile projectile = Instantiate(this.Projectile, new Vector2(0, 0), Quaternion.identity);
        projectilePool.Add(projectile);
        projectile.gameObject.SetActive(false);
        
        return projectile;
    }

    void AcquireTargetIfNecessary()
    {
        if (this._target == null)
        {
            this._target = Physics2D.OverlapCircle(this.transform.position, 5f, this._enemyLayerMask)?.transform;
        }
    }

    void TryAttack()
    {
        _currentCooldown -= Time.deltaTime;
        if (this._target == null || !(_currentCooldown <= 0f)) 
            return;
        
        this._currentCooldown = _maxCooldown;
        Attack();
    }

    void Attack()
    {
        Projectile projectile = projectilePool.FirstOrDefault(en => en.gameObject.activeSelf == false);

        if (projectile == null)
        {
            Debug.Log("NO MORE Projectiles IN LIST");
            return;
        }
        
        projectile.gameObject.SetActive(true);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = GetTargetDirection();
    }

    Quaternion GetTargetDirection()
    {
        var dir = this._target.transform.position - this.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}