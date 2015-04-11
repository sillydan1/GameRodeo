using UnityEngine;
using System.Collections;
/// <summary>
/// Random texture.cs
/// ORIGINAL AUTHOR: ASGER GITZ-JOHANSEN
/// EDITOR:
/// DESCRIPTION: This script chooses a random material from a list of materials, which
/// is assigned in the inspector.
/// 
/// </summary>
public class RandomTexture : MonoBehaviour {

	public Material[] materials; //assigned in the inspector.
	private int textureID;
	
	// Use this for initialization
	void Start () 
	{
		textureID = Random.Range(0, materials.Length);
		GetComponent<Renderer>().material = materials[textureID];
	}
}
