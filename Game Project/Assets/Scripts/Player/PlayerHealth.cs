using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth;
    private Rigidbody2D rb;

    private KnockBack knockBack;
    [SerializeField] private Animator anim;

    private float hitTimer = 0.8f;
    private bool takeDamage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        knockBack = GetComponent<KnockBack>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void PlayerTakeDamage(int amount, Vector2 hitDirection)
    {
        //timer for player getting hit so that they cant get hit over and over again.
        takeDamage = true;
        if (hitTimer == 0.8f)
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
            //knockback
            knockBack.callKnockback(hitDirection, Vector2.up, Input.GetAxisRaw("Horizontal"));
            
        }
      
    }


    private void FixedUpdate()
    {
        if(takeDamage)
        {
            hitTimer -= Time.fixedDeltaTime;
            if(hitTimer <= 0)
            {
                hitTimer = 0.8f;
                takeDamage = false;
            }
        }
    }
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
