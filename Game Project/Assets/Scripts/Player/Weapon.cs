using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;

    [SerializeField] public PlayerMovement playerMovementRef;

    private Rigidbody2D rb;

    private float shootTime = 0.1f; //limit the amount the player can shoot by time

    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
/*            shootTime -= Time.deltaTime;
            if (shootTime > 0)*/
           // {
                Shoot();
           // }
  /*          if(shootTime < 0)
            {
                shootTime = 0.1f;
            }*/
        }
    }

    private void Shoot()
    {
        float xVal = playerMovementRef.directionX;
        bool grounded = playerMovementRef.isGrounded;
        if((xVal > 0f || xVal < 0f) && grounded)
        {
            animator.SetTrigger("runshoot");
        }
        else if (rb.velocity.y > 0.1f || rb.velocity.y < -0.1f && !grounded)
        {
            animator.SetTrigger("jumpshoot");
        }

        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }
}
