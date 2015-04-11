using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public float speed = 1;
    private float xIn = 0;
    private float yIn = 0;
    private bool canDo = true;
    private Rigidbody2D myRigid;
    private Vector3 facedirection;

    public Vector3 FaceDirection
    {
        get
        {
            return facedirection;
        }
    }

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
            facedirection = new Vector3(Mathf.Lerp(myRigid.velocity.x, xIn, Time.deltaTime * 5), Mathf.Lerp(myRigid.velocity.y, yIn, Time.deltaTime * 5), 0);
            myRigid.AddForce(myDir, ForceMode2D.Force);
            #endregion
            #region lookDirection
            transform.FindChild("Graphics").LookAt(transform.position + facedirection, Vector3.back);
            //transform.LookAt(transform.position + facedirection);
            //float myAngle = Vector3.Angle(facedirection, transform.up);
            //transform.Rotate(transform.up, myAngle);

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
