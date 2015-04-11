using UnityEngine;
using System.Collections;

public class PlayerHP : MonoBehaviour 
{
    private bool canGetHit = true;
    private float hitPoints = 100;
    private PlayerInventory myInventory;
    private PlayerAbilityHandler pah;
    public float yDisp = 2;
    public GameObject healthBarPrefab;
    public GameObject myHealthBar;
    private CameraHandler mainCam;

    public float HitPoints
    {
        get
        {
            return hitPoints;
        }
    }

    private void Start()
    {
        mainCam = Camera.main.GetComponent<CameraHandler>();
        myInventory = GetComponent<PlayerInventory>();
        pah = GetComponent<PlayerAbilityHandler>();
        Vector3 hpBarPos = transform.position;
        hpBarPos.y += yDisp;
        //myHealthBar = Instantiate(healthBarPrefab, hpBarPos, Quaternion.identity) as GameObject;
        //myHealthBar.transform.parent = gameObject.transform;
    }
    /// <summary>
    /// Changes the Hit points value and returns true if the player is invulnerable or not
    /// </summary>
    /// <param name="amount">The amount of which you wish to "Add" to the hitpoints value</param>
    /// <returns>true if the hit was successfull</returns>
    public bool ChangeHP(float amount)
    {
        //Also play some hurt effects on the camera and player

        if (canGetHit)
        {
            if (amount < 0)
            {
                mainCam.PlayDamage();
            }
            hitPoints += amount;
            return true;
        }
        else
            return false;
        
    }
    public void ActivateInvincibility()
    {
        canGetHit = false;
    }
    public void DeactivateInvincibility()
    {
        canGetHit = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + yDisp, 0), 0.1f);
    }
    void Update()
    {
        myHealthBar.transform.localScale = new Vector3((hitPoints / 100) * 9.6f, myHealthBar.transform.localScale.y, myHealthBar.transform.localScale.z);
    }
}
