using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; //to be able to access player

    private void Update()
    {
        //or player.position.y
        transform.position = new Vector3(player.position.x, 1.87f , transform.position.z);
    }

}
