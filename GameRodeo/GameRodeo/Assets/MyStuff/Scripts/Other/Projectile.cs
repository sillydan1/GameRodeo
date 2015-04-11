using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    public ProjectileType myType;
    public float speed = 40;
    public GameObject acidPool;
    public GameObject deathPart;
    private Rigidbody2D myRigid;
    private bool stuck = false;
    public string myDmgType = "";
    private float timer = 0;
    private Transform stuckTrans;
    private EnemyHP hitHP;
    private Vector3 stuckOffset;

    void Start () 
    {
        myRigid = GetComponent<Rigidbody2D>();
        if (myType != ProjectileType.mine)
        {
            //Move FORWARD!
            myRigid.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if(myType == ProjectileType.flying)
        {
            timer = Time.time + 0.6f;
        }
	}
	void Update () 
    {
	    if(stuck)
        {
            if (stuckTrans != null)
            {
                transform.position = stuckTrans.position + stuckOffset;
                if (timer < Time.time)
                {
                    hitHP.ChangeHP(-2, "XY");
                    timer = Time.time + 0.5f;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        if (myType == ProjectileType.flying && timer < Time.time)
        {
            //YY - Spawn a pool of acid!
            Instantiate(acidPool, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
    void OnCollisionEnter2D(Collision2D intruder)
    {
        if(intruder.transform.tag == "Enemy")
        {
            hitHP = intruder.transform.GetComponent<EnemyHP>();

            if (myType == ProjectileType.flyingPoison)
            {
                //Get stuck and do poison!!
                GetComponent<Collider2D>().enabled = false;
                myRigid.velocity = Vector2.zero;
                stuckTrans = intruder.transform;
                stuckOffset = stuckTrans.position - transform.position;
                stuck = true;
            }
            else if (myType == ProjectileType.flying && timer < Time.time)
            {
                //YY - Spawn a pool of acid!
                Instantiate(acidPool, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if(myType == ProjectileType.mine)
            {
                if (myDmgType == "XZ" || myDmgType == "ZX")
                {
                    //Spike bomb (Splash dmg)
                    Instantiate(acidPool, transform.position, Quaternion.identity);
                    //Spawn a death particle
                    Instantiate(deathPart, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else if (myDmgType == "YZ" || myDmgType == "ZY")
                {
                    //poison bomb (pool)
                    Instantiate(acidPool, transform.position, Quaternion.identity);
                    //Spawn a death particle
                    Instantiate(deathPart, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }
}
