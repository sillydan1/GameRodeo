using UnityEngine;
using System.Collections;

public class PlayerAbilityHandler : MonoBehaviour 
{
    public float dragWhenUsing = 2;
    public float dragWhenNotUsing = 5;
    private bool isUsing = false;
    private bool pressA = false;
    private PlayerInventory myInventory;
    private Rigidbody2D myRigid;
    private PlayerMovement movement;

    public bool IsUsingAbility
    {
        get
        {
            return isUsing;
        }
    }

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        myInventory = GetComponent<PlayerInventory>();
        myRigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        pressA = Input.GetButton("UseAbility");
        //The player pressed the ability button. Set the drag lower, so that he flows farther.
        if(pressA)
        {
            if (!isUsing)
            {
                myRigid.drag = dragWhenUsing;
                movement.DisableMovement();
                isUsing = true;
            }
        }
        else if(isUsing == true)
        {
            myRigid.drag = dragWhenNotUsing;
            movement.EnableMovement();
            isUsing = false;
        }
    }
}
