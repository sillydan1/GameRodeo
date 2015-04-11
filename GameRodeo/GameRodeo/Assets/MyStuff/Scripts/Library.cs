using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CrackSw;
/// <summary>
/// NAME: Library.cs
/// AUTHOR: Asger Gitz-Johansen
/// 
/// DESCRIPTION: This script is used to store various 
/// functions and can be used is many situations.
/// -This is a modified version of the original and does not
/// contain the entire feature set
/// 
/// Most of these functions are quite simple, and can
/// be used by anyone, who would like to.
/// 
/// </summary>
namespace CrackSw
{
	public class Search
	{
		//Finds the closest gameObject with the given searchTag.
		public static GameObject ClosestGameObject (Transform ClosestTo, string searchTag)
		{
			List<Transform> outPutList = new List<Transform>(); 
			outPutList.Clear();
			GameObject[] go = GameObject.FindGameObjectsWithTag(searchTag);
			foreach(GameObject goes in go)
			{
                if (goes != null)
				    outPutList.Add(goes.transform);
			}
			outPutList.Sort(delegate(Transform t1, Transform t2) {
					return Vector3.Distance(t1.position, ClosestTo.position).CompareTo(Vector3.Distance(t2.position, ClosestTo.position));
			});
			if(outPutList.Count > 0)
			{
				GameObject output = outPutList[0].gameObject;
				return output;
			}else
			{
				return null;	
			}
		}
		//Finds the closest gameObject with the given searchList.
		public static GameObject ClosestGameObject (Transform ClosestTo, List<GameObject> searchList)
		{	
			if(searchList.Count > 0)
			{
				List<Transform> sListTrs = new List<Transform>();
				sListTrs.Clear();
				foreach(GameObject go in searchList.ToArray())
				{
                    if(go != null)
					    sListTrs.Add(go.transform); 
				}
				sListTrs.Sort(delegate(Transform t1, Transform t2) {
						return Vector3.Distance(t1.position, ClosestTo.position).CompareTo(Vector3.Distance(t2.position, ClosestTo.position));
				});
                if (sListTrs.Count > 0)
                {
                    GameObject output = sListTrs[0].gameObject;
                    return output;
                }
                else
                    return null;
			}
			else
			{
				return null;	
			}
		}
		//Finds the closest gameObject with the given searchList.
        public static GameObject ClosestGameObject(Vector3 ClosestTo, List<GameObject> searchList)
        {
            if (searchList.Count > 0)
            {
                List<Transform> sListTrs = new List<Transform>();
                sListTrs.Clear();
                foreach (GameObject go in searchList.ToArray())
                {
                    if (go != null)
                        sListTrs.Add(go.transform);
                }
                sListTrs.Sort(delegate(Transform t1, Transform t2)
                {
                    return Vector3.Distance(t1.position, ClosestTo).CompareTo(Vector3.Distance(t2.position, ClosestTo));
                });
                if (sListTrs.Count > 0)
                {
                    GameObject output = sListTrs[0].gameObject;
                    return output;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        //Excludes a gameObject from the original searchTag
        public static GameObject ClosestGameObject(Transform ClosestTo, string searchTag, GameObject exclude)
        {
            List<Transform> outPutList = new List<Transform>();
            outPutList.Clear();
            GameObject[] go = GameObject.FindGameObjectsWithTag(searchTag);
            foreach (GameObject goes in go)
            {
                if(goes.gameObject.GetInstanceID() != exclude.GetInstanceID())
                {
                    if (goes != null)
                        outPutList.Add(goes.transform);
                }
            }
            outPutList.Sort(delegate(Transform t1, Transform t2)
            {
                return Vector3.Distance(t1.position, ClosestTo.position).CompareTo(Vector3.Distance(t2.position, ClosestTo.position));
            });
            if (outPutList.Count > 0)
            {
                GameObject output = outPutList[0].gameObject;
                return output;
            }
            else
            {
                return null;
            }
        }
	}
}