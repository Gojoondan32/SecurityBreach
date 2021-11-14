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

    [SerializeField] private bool isMoving;
    Vector3 lastPos;

    [SerializeField] private bool enemiesInSight;
    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemies");
        isMoving = false;
        enemiesInSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastPos != transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPos = transform.position;

        AttackEnemies();

        if (!isMoving && !enemiesInSight)
        {
            Rotate();
        }
        
    }

    private void AttackEnemies()
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, range, enemyMask);

        //Check if there are enemies in the field of view
        if (enemiesHit.Length == 0)
        {
            
            enemiesInSight = false;
            
        }

        foreach (Collider enemies in enemiesHit)
        {
            //Check if enemies exist in the collider
            enemiesInSight = true;
            Transform currentEnemy = enemies.gameObject.GetComponent<Transform>();
            
            if(Time.time >= nextAttackAllowed)
            {
                //Set the bullets position to the current enemy
                BulletScript.target = currentEnemy;

                //Spawn the bullet
                Instantiate(bulletPrefab, bulletSpawn);

                nextAttackAllowed = Time.time + attackDelay;
                
            }
            
        }
    }

    private void Rotate()
    {
        //Rotate in a circle
        transform.RotateAround(transform.position, transform.up, 45 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
