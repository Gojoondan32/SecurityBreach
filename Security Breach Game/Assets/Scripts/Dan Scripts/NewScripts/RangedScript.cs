using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedScript : MonoBehaviour
{
    public Transform attackPoint;
    public Transform bulletPrefab;
    public Transform bulletSpawn;

    [SerializeField] private float range = 5f;

    private LayerMask enemyMask;

    private float nextAttackAllowed = -1f;
    [SerializeField] private float attackDelay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        AttackEnemies();
    }

    private void AttackEnemies()
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, range, enemyMask);

        foreach (Collider enemies in enemiesHit)
        {
            Transform currentEnemy = enemies.gameObject.GetComponent<Transform>();
            
            if(Time.time >= nextAttackAllowed)
            {
                BulletScript.target = currentEnemy;

                Instantiate(bulletPrefab, bulletSpawn);

                nextAttackAllowed = Time.time + attackDelay;
                
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
