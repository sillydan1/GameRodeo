using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour 
{
    public enum mode
    {
        allRandomAxes,
        allAxes,
        particleSize
    }
    public mode Mode = new mode();
    public float min = 0;
    public float max = 1;

	void Start () 
    {
        if(Mode == mode.allAxes)
        {
            float rN = Random.Range(min, max);
            transform.localScale = new Vector3(rN, rN, rN);
        }
        else if (Mode == mode.allRandomAxes)
        {
            float rN1 = Random.Range(min, max);
            float rN2 = Random.Range(min, max);
            float rN3 = Random.Range(min, max);
            transform.localScale = new Vector3(rN1, rN2, rN3);
        }
        else if (Mode == mode.particleSize)
        {
            float rN = Random.Range(min, max);
            GetComponent<ParticleSystem>().startSize = rN;
        }
	}
}
