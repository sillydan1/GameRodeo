using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour 
{
    public Vector3 axis = new Vector3(0,0,-1);

	void Start () 
    {
        float rnd = Random.Range(0f, 360f);
        transform.Rotate(axis, rnd);
	}
}
