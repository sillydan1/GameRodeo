using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour 
{
    public PowerUpType myType;

    void OnTriggerEnter2D (Collider2D intruder)
    {
        if(intruder.tag == "Player")
        {
            intruder.GetComponent<PlayerInventory>().PickUpNewPowerUp(myType);
            Destroy(gameObject);
        }
    }
}
