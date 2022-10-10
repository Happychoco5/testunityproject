using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerScript : MonoBehaviour
{
    [Header("GameObjects used in Tower")]
    private Transform _target;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Tower Attributes")]
    public string enemysTag = "Enemy";
    public float range = 7f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;


    void Start()
    {


        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }



    void UpdateTarget()
    {
        /* Creates an array of GameObjects that finds GameObjects with the tag Enemy
            Creates a temporary variable that stores the shortest distance we found to an enemy so far
            Set the GameObject to null by default then puts the GameObject we found in it's place */

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemysTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            /* are finding all of our enemies in the array and then for each enemy that we found and we want to 
             see if this distance is the shortest distance we found yet and if it is set it to the shortest distance
             */

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //If we found some enemy and they are with in our range then set that as our target

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }


    void Update()
    {
        if (_target == null)
            return;

        if (fireCountdown <= 0f)
        {
            TowerShoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void TowerShoot()
    {

        // Casting the gameObject so that it can be cached and used for reference
        GameObject bulletGO;
        bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TowerProjectile bullet = bulletGO.GetComponent<TowerProjectile>();

        if (bullet != null)
            bullet.Seek(_target);
    }
}
