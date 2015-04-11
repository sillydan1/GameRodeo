using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour 
{
    public float hitPoints = 100;
    public string weakness = "";
    public string weakness2 = "";
    public float yDisp = 2;
    public GameObject healthBarPrefab;
    private GameObject myHealthBar;
    public GameObject deathPart;
    private SpawnSystem mySpawner;

    public void AssigneSpawner(SpawnSystem spawmer)
    {
        mySpawner = spawmer;
    }
    void Start()
    {
        Vector3 hpBarPos = transform.position;
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
                Destroy(gameObject);
                Instantiate(deathPart, transform.position, Quaternion.identity);
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
