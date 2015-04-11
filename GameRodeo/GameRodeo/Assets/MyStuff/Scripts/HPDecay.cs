using UnityEngine;
using System.Collections;

public class HPDecay : MonoBehaviour 
{
    public float speed = 10;
    private EnemyHP myHP;
    private float timer = 0;

    void Start()
    {
        myHP = GetComponent<EnemyHP>();
    }
	void Update () 
    {
	    if(timer < Time.time)
        {
            myHP.ChangeHP(-speed / 4, myHP.weakness + myHP.weakness2);
            timer = Time.time + 0.25f;
        }
	}
}
