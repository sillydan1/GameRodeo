using UnityEngine;
using System.Collections;

public class DieOverTime : MonoBehaviour {
	
	public float time = 3; //seconds
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(wait());
	}
    IEnumerator wait ()
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
