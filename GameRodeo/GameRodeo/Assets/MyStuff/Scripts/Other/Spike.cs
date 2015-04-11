using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour 
{
    void OnTriggerEnter2D (Collider2D intruder)
    {
        if(intruder.tag == "Enemy")
        {
            //Damage the fucker!
            intruder.GetComponent<EnemyHP>().ChangeHP(-60, "XX");
            //Check if we CAN knock the thing away.
            if (intruder.GetComponent<BaseAI>().myBehavior != AIBehavior.boss)
            {
                //Then knock him away!
                intruder.GetComponent<Rigidbody2D>().AddForce(transform.up * 30, ForceMode2D.Impulse);
            }
        }
    }
}
