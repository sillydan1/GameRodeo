using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour 
{
    private float hitPoints = 100;
    public string weakness = "";
    public string weakness2 = "";

    /// <summary>
    /// Changes the Hit points value and returns true if the enemy is invulnerable or not
    /// </summary>
    /// <param name="amount">The amount of which you wish to "Add" to the hitpoints value</param>
    /// <returns>true if the hit was successfull</returns>
    public bool ChangeHP(float amount, string combo)
    {
        if (combo == weakness || weakness == "")
        {
            hitPoints += amount;
            if (hitPoints <= 0)
                Destroy(gameObject);
            return true;
        }
        else
            return false;
    }
}
