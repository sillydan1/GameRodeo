using UnityEngine;
using System.Collections;

public class ColorAnimator : MonoBehaviour 
{
    public bool fastUpdate = false;
    public Renderer animated;
    public Color colour = new Color();

	// Update is called once per frame
	void Update () 
    {
        if (animated.material.color != colour && fastUpdate == false)
        {
            animated.material.color = colour;
        }
	}
    void OnGUI()
    {
        if (fastUpdate == true)
        {
            if (animated.material.color != colour)
            {
                animated.material.color = colour;
            }
        }
    }
}
