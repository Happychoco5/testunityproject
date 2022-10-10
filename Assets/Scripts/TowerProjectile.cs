using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float speed = 70f;
    public float damage;

    void Update()
    {
        //Destroys the bullet if target is 
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Bullet needs to look in the direction of its target 
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //makes it so the bullet doesn't move past the target
        //dir is the current distance to our target and if the distance is 
        //less than the distance to our target we hit something 
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<BaseEnemyScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void HitTarget()
    {
        
    }


    public void Seek(Transform _target)
    {
        target = _target;
    }
}
