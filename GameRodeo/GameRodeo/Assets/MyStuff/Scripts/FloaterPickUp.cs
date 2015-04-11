using UnityEngine;
using System.Collections;

public class FloaterPickUp : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D intruder)
    {
        if(intruder.tag == "Player")
        {
            PlayerHP php = intruder.GetComponent<PlayerHP>();
            if(php.HitPoints < 100)
            {
                php.ChangeHP(5);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
