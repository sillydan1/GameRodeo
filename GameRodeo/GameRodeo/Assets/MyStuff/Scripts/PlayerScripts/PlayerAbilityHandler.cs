using UnityEngine;
using System.Collections;

public class PlayerAbilityHandler : MonoBehaviour 
{
    public float dragWhenUsing = 2;
    public float dragWhenNotUsing = 5;
    private bool isUsing = false;
    private bool pressA = false;
    private bool haveUsed = false;
    private PlayerInventory myInventory;
    private Rigidbody2D myRigid;
    private PlayerMovement movement;
    private PlayerHP php;
    public GameObject[] projectiles;

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
        php = GetComponent<PlayerHP>();
    }
    private void Update()
    {
        pressA = Input.GetButton("UseAbility");
        string xyz = myInventory.GetXYZValue();
        //The player pressed the ability button... 
        if (pressA)
        {
            XYZWHandling(xyz);
        }
        else if (isUsing == true && (xyz == "XX" || xyz == "XW" || xyz == "ZZ" || xyz == "ZW"))
        {
            myRigid.drag = dragWhenNotUsing;
            movement.EnableMovement();
            isUsing = false;
            if((xyz == "XX" || xyz == "XW"))
            {
                transform.FindChild("Graphics").GetComponent<Animation>().Play("RetractSpikes");
            }
        }
        if(!pressA)
        {
            if(haveUsed)
                haveUsed = false;

            php.DeactivateInvincibility();
        }
    }
    private void XYZWHandling(string XYZVal)
    {
        if (XYZVal == "XX" || XYZVal == "XW")
        {
            XXPlay(); //Spikes only
        }
        else if (XYZVal == "YY" || XYZVal == "YW")
        {
            YYPlay(); //Acid Only
        }
        else if (XYZVal == "ZZ" || XYZVal == "ZW")
        {
            ZZPlay(); //Shield only
        }
        else if (XYZVal == "XY" || XYZVal == "YX")
        {
            XYPlay(); //Spikes and acid
        }
        else if (XYZVal == "XZ" || XYZVal == "ZX")
        {
            XZPlay(XYZVal); //Spikes and shield
        }
        else if (XYZVal == "YZ" || XYZVal == "ZY")
        {
            YZPlay(XYZVal); //acid and shield
        }
        else if (XYZVal.Contains("?"))
            throw new System.Exception("Something has fucked up!!!!");
    }
    private void XXPlay()
    {
        //Spikes only!
        //Set the drag lower, so that he flows farther.
        if (!isUsing)
        {
            myRigid.drag = dragWhenUsing;
            movement.DisableMovement();
            isUsing = true;
            transform.FindChild("Graphics").GetComponent<Animation>().Play("ExtendSpikes");
        }
    }
    private void YYPlay()
    {
        if (!haveUsed)
        {
            //Acid only
            //Spit an acid only projectile. (projectiles[0])
            GameObject clone = Instantiate(projectiles[0], transform.position + (movement.FaceDirection.normalized * 2f), Quaternion.identity) as GameObject;
            float myAngle = Vector3.Angle(movement.FaceDirection, clone.transform.up);
            clone.transform.Rotate(Vector3.back, myAngle);
            //clone.transform.LookAt(transform.position + movement.FaceDirection, Vector3.back);
            haveUsed = true;
        }
    }
    private void ZZPlay()
    {
        //Shield only
        //This is much like the spikes, just that the player don't do damage.
        if(!isUsing)
        {
            myRigid.drag = dragWhenUsing;
            movement.DisableMovement();
            isUsing = true;
            php.ActivateInvincibility();
        }
    }
    private void XYPlay()
    {
        if (!haveUsed)
        {
            //Acid spike
            //Spit an acid spike projectile. (projectiles[1])
            GameObject clone = Instantiate(projectiles[1], transform.position + (movement.FaceDirection.normalized * 2f), Quaternion.identity) as GameObject;
            float myAngle = Vector3.Angle(movement.FaceDirection, clone.transform.up);
            if (movement.FaceDirection.x < 0)
                myAngle = -myAngle;

            clone.transform.Rotate(Vector3.back, myAngle);
            haveUsed = true;
        }
    }
    private void XZPlay(string xyzw)
    {
        if (!haveUsed)
        {
            //Spike mine
            //Spawn an spike mine projectile. (projectiles[2])
            GameObject clone = Instantiate(projectiles[2], transform.position + (movement.FaceDirection.normalized * 2f), Quaternion.identity) as GameObject;
            clone.GetComponent<Projectile>().myDmgType = xyzw;
            haveUsed = true;
        }
    }
    private void YZPlay(string xyzw)
    {
        if (!haveUsed)
        {
            //Poison mine
            //Spawn an acid bomb projectile. (projectiles[3])
            GameObject clone = Instantiate(projectiles[3], transform.position + (movement.FaceDirection.normalized * 2f), Quaternion.identity) as GameObject;
            clone.GetComponent<Projectile>().myDmgType = xyzw;
            haveUsed = true;
        }
    }
}
