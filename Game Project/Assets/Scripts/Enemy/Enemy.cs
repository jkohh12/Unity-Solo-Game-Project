using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;

    [SerializeField] GameObject deathEffect;
    private GameObject createDeathEffect;

    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerMovement playerMovement;

    private int mushroomDamage = 10;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        createDeathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(createDeathEffect, 1);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //playerMovement.KBCounter = playerMovement.KBTotalTime; // the amount of time the player will be knocked back
            if (collision.transform.position.x <= transform.position.x) //if player is on the left, hit from right
            {
                playerHealth.PlayerTakeDamage(mushroomDamage, -transform.right);
            }
            else if (collision.transform.position.x >= transform.position.x)
            {
                playerHealth.PlayerTakeDamage(mushroomDamage, transform.right);
            }
            
        }
    }
}
