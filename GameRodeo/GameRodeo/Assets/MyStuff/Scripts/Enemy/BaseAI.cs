using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour 
{
    public AIBehavior myBehavior;
    public float speed = 0.9f;
    public float attackRange = 5;
    public float damage = 15;
    public float visibleRange = 10;
    public float attackDroneSpawnDistance = 10;
    public float maxVisible = 10;
    public GameObject attackers;
    public int[] weaknesses;
    private float interval = 0;
    private float oldCamSize = 0;
    private bool haveAttacked = false;
    private GameObject myObject;
    private Rigidbody2D myRigid;

    void Start()
    {
        if (myBehavior == AIBehavior.floater)
        {
            myObject = new GameObject("FloaterFollowObject");
            myObject.transform.parent = gameObject.transform;
        }
        else
            myObject = GameObject.FindGameObjectWithTag("Player");

        oldCamSize = Camera.main.orthographicSize;
        myRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(myBehavior == AIBehavior.floater)
        {
            #region Floater
            //Float around randomly.
            //We achieve this by having a position of which we follow change position in an irregular interval
            if (interval < Time.time)
            {
                myObject.transform.position = new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f));
                interval = Time.time + Random.Range(2f, 4f);
            }
            //follow the object
            Vector2 myDir = myObject.transform.position - transform.position;
            myDir.Normalize();
            myDir *= speed;
            myRigid.AddForce(myDir, ForceMode2D.Force);
            #endregion
        }
        if(myBehavior == AIBehavior.aggressive)
        {
            #region Aggressive
            //Move Toward the player!
            if (!haveAttacked)
            {
                Vector2 myDir = myObject.transform.position - transform.position;
                myDir.Normalize();
                myDir *= speed;
                myRigid.AddForce(myDir, ForceMode2D.Force);
                //If you get close enough, Damage him and self destruct!
                float dist = Vector2.Distance(transform.position, myObject.transform.position);
                if (dist < attackRange)
                {
                    bool didHit = myObject.GetComponent<PlayerHP>().ChangeHP(-damage);
                    //Spawn an explotion or whatever...
                    if (didHit)
                        Destroy(gameObject);
                    else
                    {
                        //Flee for a while
                        haveAttacked = true;
                        interval = Time.time + 3;
                    }
                }
            }
            else
            {
                //Flee
                Vector2 myDir = transform.position - myObject.transform.position;
                myDir.Normalize();
                myDir *= speed / 1.5f;
                myRigid.AddForce(myDir, ForceMode2D.Force);
                //Check if we want to still flee or not
                if(interval < Time.time)
                {
                    haveAttacked = false;
                }
            }
            #endregion
        }
        if(myBehavior == AIBehavior.boss)
        {
            #region Boss
            //Float toward the player untill he is in attack range.
            if (Vector2.Distance(transform.position, myObject.transform.position) > attackRange)
            {
                Vector2 myDir = myObject.transform.position - transform.position;
                myDir.Normalize();
                myDir *= speed;
                myRigid.AddForce(myDir, ForceMode2D.Force);
            }
            else //The boss is within attack range.
            {
                //RELEASE THE DRONES!!!
                //Before we can do so, we need to figure out where to spawn them...
                //We do that by finding the vector towards the player and giving it a certain length, then randomize it a bit, to widen the spawn area
                if (!haveAttacked)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Vector3 randomPlayerPos = new Vector3(myObject.transform.position.x + Random.Range(-7f, 7f), myObject.transform.position.y + Random.Range(-7f, 7f), transform.position.z);
                        Vector3 midSpawnArea = randomPlayerPos - transform.position;
                        midSpawnArea.Normalize();
                        
                        midSpawnArea *= (attackDroneSpawnDistance + 1);
                        midSpawnArea += transform.position;
                        GameObject clone = Instantiate(attackers, midSpawnArea, Quaternion.identity) as GameObject;
                        clone.GetComponent<Rigidbody2D>().AddForce((midSpawnArea - transform.position).normalized * Random.Range(12f, 20f), ForceMode2D.Impulse);
                    }
                    haveAttacked = true;
                    interval = Time.time + 4;
                }
                if(haveAttacked)
                {
                    if(interval < Time.time)
                    {
                        //Reset the ability to attack.
                        haveAttacked = false;
                    }
                }
            }
            //Zoom out when the player is within the visibleRange range.
            float dist = Vector2.Distance(transform.position, myObject.transform.position);
            if (dist < visibleRange)
            {
                float diff = visibleRange - dist;
                Camera.main.orthographicSize = Mathf.Lerp(oldCamSize, maxVisible, diff * 0.05f);
            }
            #endregion
        }
    }
    void OnDrawGizmos()
    {
        if(myBehavior == AIBehavior.boss)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, visibleRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackDroneSpawnDistance);
        }
        else if(myBehavior == AIBehavior.aggressive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
