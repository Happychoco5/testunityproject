using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    public PortalScript myPortal;

    public float health = 20;

    public float speed = 1;

    public int currPoint = 0;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, myPortal.points[currPoint].position, step);

        if (Vector3.Distance(transform.position, myPortal.points[currPoint].position) < 0.001f)
        {
            if(currPoint < myPortal.points.Length - 1)
            {
                currPoint++;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
