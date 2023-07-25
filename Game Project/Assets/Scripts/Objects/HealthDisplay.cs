using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public PlayerHealth playerhealth;
    public Text healthText;

    private void Update()
    {

        healthText.text = playerhealth.currentHealth.ToString();
        if (playerhealth.currentHealth < 0)
        {
            healthText.text = "0";
        }
    }

}
