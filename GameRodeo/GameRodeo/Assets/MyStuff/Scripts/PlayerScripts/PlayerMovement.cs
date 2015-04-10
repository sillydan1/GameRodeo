using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public float speed = 1;
    private float xIn = 0;
    private float yIn = 0;
    private bool canDo = true;
    private Rigidbody2D myRigid;

	void Start () 
    {
        myRigid = GetComponent<Rigidbody2D>();
	}
	void Update ()
    {
        if (canDo)
        {
            #region Movement
            xIn = Input.GetAxis("Horizontal");
            yIn = Input.GetAxis("Vertical");
            Vector3 myDir = new Vector3(xIn, yIn, 0) * speed;
            myRigid.AddForce(myDir, ForceMode2D.Force);
            #endregion
            #region lookDirection
            //transform.FindChild("GFX").LookAt(transform.position - (transform.position + myDir));
            #endregion
        }
    }
    public void DisableMovement()
    {
        canDo = false;
    }
    public void EnableMovement()
    {
        canDo = true;
    }
}
