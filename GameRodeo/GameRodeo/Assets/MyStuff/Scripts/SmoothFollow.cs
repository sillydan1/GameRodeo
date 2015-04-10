using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour 
{
    public Transform target;
    public float speed = 0.5f;
    public float yDisplacement = 1;

	void Update () 
    {
        //calculations...
        Vector3 direction = new Vector3(target.position.x - transform.position.x, (target.position.y + yDisplacement) - transform.position.y, 0) * speed;
        transform.Translate(direction);
	}
}
