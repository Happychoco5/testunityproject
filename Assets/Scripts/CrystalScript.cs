using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    public float health;

    public void DamageHealth(float damage)
    {
        //damage crystal
        //check if health is below 0

        health -= damage;
        if(health <= 0)
        {
            Debug.Log("You lose.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            DamageHealth(collision.collider.GetComponent<BaseEnemyScript>().damage);
            Destroy(collision.gameObject);
        }
    }
}
