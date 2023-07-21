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


    [SerializeField] private float shootRate = 5f;
    private float shootTime = 0f; //limit the amount the player can shoot by time

    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Time.time >= shootTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                shootTime = Time.time + 1f / shootRate;

            }
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
