using UnityEngine;
using System.Collections;

public class PlayerHP : MonoBehaviour 
{
    private float hitPoints = 100;
    private PlayerInventory myInventory;

    private void Start()
    {
        myInventory = GetComponent<PlayerInventory>();
    }
    public void ChangeHP(float amount)
    {

    }
}
