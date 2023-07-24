using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private Rigidbody2D rb;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void PlayerTakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0) 
        {
            anim.SetTrigger("takeDamage");
        }

        else if (currentHealth <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static; //player cant move after dead
            anim.SetTrigger("isDead");

           //Destroy(gameObject);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
