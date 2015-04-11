using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHP : MonoBehaviour 
{
    public float hitPoints = 100;
    public string weakness = "";
    public string weakness2 = "";
    public float yDisp = 2;
    public GameObject healthBarPrefab;
    private GameObject myHealthBar;
    public GameObject deathPart;
    public GameObject reward;
    private SpawnSystem mySpawner;
    private BaseAI myAI;

    public void AssigneSpawner(SpawnSystem spawmer)
    {
        mySpawner = spawmer;
    }
    void Start()
    {
        Vector3 hpBarPos = transform.position;
        myAI = GetComponent<BaseAI>();
        hpBarPos.y += yDisp;
        myHealthBar = Instantiate(healthBarPrefab, hpBarPos, Quaternion.identity) as GameObject;
        myHealthBar.transform.parent = gameObject.transform;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + yDisp, 0), 0.1f);
    }
    /// <summary>
    /// Changes the Hit points value and returns true if the enemy is invulnerable or not
    /// </summary>
    /// <param name="amount">The amount of which you wish to "Add" to the hitpoints value</param>
    /// <returns>true if the hit was successfull</returns>
    public bool ChangeHP(float amount, string combo)
    {
        if (combo.Contains(weakness) || weakness == "")
        {
            hitPoints += amount;
            if (hitPoints <= 0)
            {
                if(mySpawner != null)
                    mySpawner.RemoveOne(gameObject);

                if(myAI.myBehavior == AIBehavior.boss)
                {
                    Camera.main.GetComponent<CameraHandler>().ChangeSizeTo(myAI.OriginalCamSize, 4);
                    for (int i = 0; i < 5; i++)
                    {
                        Vector3 pos = transform.position;
                        pos.x += Random.Range(-2f, 2f);
                        pos.y += Random.Range(-2f, 2f);
                        GameObject clone = Instantiate(reward, pos, Quaternion.identity) as GameObject;
                        clone.GetComponent<Rigidbody2D>().AddForce((pos - transform.position).normalized * 5, ForceMode2D.Impulse);

                        float myAngle = Vector3.Angle((pos - transform.position), clone.transform.up);
                        if ((pos - transform.position).normalized.x < 0)
                            myAngle = -myAngle;

                        clone.transform.Rotate(Vector3.back, myAngle);
                    }
                }
                Instantiate(deathPart, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            return true;
        }
        else
            return false;
    }
    void Update()
    {
        if (myHealthBar.name.Contains("Boss"))
        {
            myHealthBar.transform.localScale = new Vector3((hitPoints / 100) * 0.5f, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
        }
        else
        {
            myHealthBar.transform.localScale = new Vector3((hitPoints / 100) * 4, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
        }
    }
}
