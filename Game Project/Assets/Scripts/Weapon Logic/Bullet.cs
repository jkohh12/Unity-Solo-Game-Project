using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 40;
    [SerializeField] private float speed = 25f;
    [SerializeField] private Rigidbody2D rb;

  
    [SerializeField] private GameObject impactEffect;
    private GameObject createImpactEffect;
    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.right * (speed);

    }




    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        createImpactEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        //how to delete impactEffect?
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
        Destroy(createImpactEffect, 1);
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }



}
