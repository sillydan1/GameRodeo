using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSystem : MonoBehaviour 
{
	//This script is attached to the player.
    //It has 2 radie.
    //One for despawning (Large)
    //One for spawning (smaller)
    
    public float despawnRadius = 180;
    public float respawnRadius = 100;
    public int maxAmount = 10;
    public bool populateFromStart = false;
    public bool clump = false;
    public int clumpAmount = 5;
    public GameObject[] allPrefabs; //A list of all of the objects that can be spawned
    public float timer = 0;
    private List<GameObject> allObjects; //A list of all of the objects that HAS been spawned
    private List<GameObject> removeThese;
    public int amountOfObjectsRightNow = 0;
    //Layer is at what layer this spawnsystem is. It also dictates what Z position it will spawn the objects
    public int layer;

    public void RemoveOne(GameObject you)
    {
        if (allObjects.Contains(you))
            allObjects.Remove(you);
        amountOfObjectsRightNow--;
    }
    private void Start()
    {
        allObjects = new List<GameObject>();
        removeThese = new List<GameObject>();
        if(populateFromStart)
        {
            for (int i = 0; i < maxAmount; i++)
            {
                int rnd = Random.Range(0, allPrefabs.Length);
                Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                //pos.Normalize();
                pos *= Random.Range(0f, despawnRadius - 0.5f);
                pos.z = layer;
                pos.x += transform.position.x;
                pos.y += transform.position.y;
                GameObject clone = Instantiate(allPrefabs[rnd], pos, Quaternion.identity) as GameObject;
                if(clone.tag == "Enemy")
                    clone.GetComponent<EnemyHP>().AssigneSpawner(this);

                allObjects.Add(clone);
                amountOfObjectsRightNow++;
            }
        }
    }
    private void Update()
    {
        if (true)//timer < Time.time)
        {
            //It's time to do something...
            if (amountOfObjectsRightNow < maxAmount)
            {
                if (!clump)
                {
                    //Simply spawn one more thing.
                    int rnd = Random.Range(0, allPrefabs.Length);
                    
                    Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    //pos.Normalize();
                    pos *= respawnRadius;
                    pos.z = layer;
                    pos.x += transform.position.x;
                    pos.y += transform.position.y;
                    GameObject clone = Instantiate(allPrefabs[rnd], pos, Quaternion.identity) as GameObject;
                    if (clone.tag == "Enemy")
                        clone.GetComponent<EnemyHP>().AssigneSpawner(this);

                    allObjects.Add(clone);
                    amountOfObjectsRightNow++;
                    timer = Time.time + 2;
                }
                else
                {
                    //Spawn quite a puch of shit
                    int rnd = Random.Range(0, allPrefabs.Length);
                    Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    //pos.Normalize();
                    pos *= respawnRadius;
                    pos.z = layer;
                    pos.x += transform.position.x;
                    pos.y += transform.position.y;
                    for (int i = 0; i < clumpAmount; i++)
                    {
                        Vector3 newPos = pos;
                        newPos.x += Random.Range(-2f, 2f);
                        newPos.y += Random.Range(-2f, 2f);
                        GameObject clone = Instantiate(allPrefabs[rnd], newPos, Quaternion.identity) as GameObject;
                        if (clone.tag == "Enemy")
                            clone.GetComponent<EnemyHP>().AssigneSpawner(this);

                        allObjects.Add(clone);
                        amountOfObjectsRightNow++;
                    }
                    timer = Time.time + 2;
                }
            }
            foreach(GameObject obj in allObjects)
            {
                if (obj != null)
                {
                    Vector2 myPos = transform.position;
                    Vector2 hisPos = obj.transform.position;
                    if (Vector2.Distance(myPos, hisPos) > despawnRadius)
                    {
                        //Destroy the object.
                        removeThese.Add(obj);
                        amountOfObjectsRightNow--;
                    }
                }
            }
            foreach(GameObject obj in removeThese)
            {
                allObjects.Remove(obj);
                Destroy(obj);
                
            }
            removeThese.Clear();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, respawnRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, despawnRadius);
    }
}
