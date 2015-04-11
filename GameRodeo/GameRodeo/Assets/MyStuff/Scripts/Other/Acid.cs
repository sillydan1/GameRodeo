using UnityEngine;
using System.Collections;

public class Acid : MonoBehaviour 
{
    //This is a DOT (Damage over time) effect. Everything within the radius will get damaged. (Excluding the player)
    private float dotTimer = 0;
    public string dmgType = "XX";
    public float damage = -10;
    public float dmgRadius = 5;

    private void Update()
    {
        if(dotTimer < Time.time)
        {
            //Do an overlapsphere2D
            Collider2D[] thingsInRange = Physics2D.OverlapCircleAll(transform.position, dmgRadius);
            if(thingsInRange.Length > 0)
            {
                for (int i = 0; i < thingsInRange.Length; i++)
                {
                    if(thingsInRange[i].tag == "Enemy")
                    {
                        thingsInRange[i].GetComponent<EnemyHP>().ChangeHP(damage, dmgType);
                    }
                }
            }
            dotTimer = Time.time + 0.5f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dmgRadius);
    }
}
