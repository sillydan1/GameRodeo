using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour 
{
    public AIBehavior myBehavior;
    public float attackRange = 5;
    public float damage = 15;
    public float visibleRange = 10;
    public float attackDroneSpawnDistance = 10;
    public float maxVisible = 10;
    public GameObject attackers;
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
            myDir *= 0.3f;
            myRigid.AddForce(myDir, ForceMode2D.Force);
            #endregion
        }
        if(myBehavior == AIBehavior.aggressive)
        {
            #region Aggressive
            //Move Toward the player!
            Vector2 myDir = myObject.transform.position - transform.position;
            myDir.Normalize();
            myDir *= 30f;
            myRigid.AddForce(myDir, ForceMode2D.Force);
            //If you get close enough, Damage him and self destruct!
            float dist = Vector2.Distance(transform.position, myObject.transform.position);
            if(dist < attackRange)
            {
                myObject.GetComponent<PlayerHP>().ChangeHP(-damage);
                //Spawn an explotion or whatever...

                Destroy(gameObject);
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
                myDir *= 0.8f;
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
                        Vector3 randomPlayerPos = new Vector3(myObject.transform.position.x + Random.Range(-4f, 4f), myObject.transform.position.y + Random.Range(-4f, 4f), transform.position.z);
                        Vector3 midSpawnArea = randomPlayerPos - transform.position;
                        midSpawnArea.Normalize();
                        midSpawnArea *= (attackDroneSpawnDistance - 1);
                        Instantiate(attackers, midSpawnArea, Quaternion.identity);
                    }
                    haveAttacked = true;
                    interval = Time.time + 10;
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
