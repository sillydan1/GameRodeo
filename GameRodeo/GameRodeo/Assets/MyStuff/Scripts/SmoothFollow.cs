using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour 
{
    public Transform target;
    public float speed = 0.5f;
    public float maxLength = 2f;
    public float yDisplacement = 1;

	void Update () 
    {
        //calculations...
        //Vector3 direction = new Vector3(target.position.x - transform.position.x, (target.position.y + yDisplacement) - transform.position.y, 0) * speed;
        //transform.Translate(direction);

        transform.position += (target.position - transform.position) * Time.deltaTime * 1;
        if (Vector3.Distance(transform.position, target.position) >= maxLength)
        {
            Vector3 dir = transform.position - target.position;
            dir.Normalize();
            transform.position = target.position + dir * maxLength;
            transform.position = new Vector3(transform.position.x, transform.position.y, -23.24f);
        }
	}
}
